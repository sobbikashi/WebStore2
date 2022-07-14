﻿using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces;

namespace WebStore.WebAPI.Controllers
{
    [Route(WebAPIAddresses.V1.Values)] //Если делаешь API-контроллер, ОБЯЗАТЕЛЬНО прописывать маршрут к нему, MVC системы нет!!! Никто не пропишет сам!!!
    [ApiController] //http://localhost:5001/api/values  это адрес нашего API
    public class ValuesController : ControllerBase
    {
        private static readonly Dictionary<int, string> _Values = Enumerable.Range(1, 10)
            .Select(i => (Id: i, Value: $"Value-{i}"))
            .ToDictionary(v => v.Id, v => v.Value);
        private ILogger<ValuesController> _Logger;

        public ValuesController(ILogger<ValuesController> Logger) => _Logger = Logger;

        [HttpGet] //ОБЯЗАТЕЛЬНО для каждого контроллера прописывать метод доступа к нему!!! MVC системы снова нет!!!
        public IActionResult GetAll()
        {
            var values = _Values.Values;
            return Ok(values);
        }

        [HttpGet("{Id}")]   // Если есть параметр - описать его в методе доступа!! Соскучился уже по MVC?
        public IActionResult GetById(int Id)
        {
            if (_Values.TryGetValue(Id, out var value))
                return Ok(value);
            return NotFound(new {Id});
        }
        [HttpGet("count")]
        public int Count() => _Values.Count;

        [HttpPost]              // POST -> http://localhost:5001/api/values 
        [HttpPost("add")]       // POST -> http://localhost:5001/api/values/add
        public IActionResult Add([FromBody] string Value)
        {
            var id = _Values.Count == 0 ? 1 : _Values.Keys.Max() + 1;
            _Values[id] = Value;
            _Logger.LogInformation("Добавлено значение {0} c Id {1}", Value, id);
            return CreatedAtAction(nameof(GetById), new { id }, Value);
        }

        [HttpPut("Id")]         // PUT -> http://localhost:5001/api/values
        public IActionResult Edit(int Id, [FromBody] string Value)
        {
            if (!_Values.ContainsKey(Id)) 
            {
                _Logger.LogWarning("Попытка редактирования отсутствующего значения с id:{0}", Id);
                return NotFound(new { Id });
            }
            var old_value = _Values[Id];
            _Values[Id] = Value;
            _Logger.LogInformation("Выполнено изменение значения с id:{0} с {1} на {2}", Id, old_value, Value);
            return Ok(new {Value});
        }

        [HttpDelete("{Id}")]    // DELETE -> http://localhost:5001/api/values/42
        public IActionResult Delete(int Id)
        {
            if (!_Values.ContainsKey(Id)) 
            {
                _Logger.LogWarning("Попытка удаления отсутствующего значения с id:{0}", Id);
                return NotFound(new { Id }); 

            }
               
            var value = _Values[Id];
            _Values.Remove(Id);

            _Logger.LogInformation("Значение {0} с id:{1} удалено", value, Id);
            return Ok(new {Value = value });
        }
    }
}
