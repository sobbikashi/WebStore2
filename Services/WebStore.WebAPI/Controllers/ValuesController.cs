﻿using Microsoft.AspNetCore.Mvc;

namespace WebStore.WebAPI.Controllers
{
    [Route("api/values")] //Если делаешь API-контроллер, ОБЯЗАТЕЛЬНО прописывать маршрут к нему, MVC системы нет!!! Никто не пропишет сам!!!
    [ApiController] //http://localhost:5001/api/values  это адрес нашего API
    public class ValuesController : ControllerBase
    {
        private static readonly Dictionary<int, string> _Values = Enumerable.Range(1, 10)
            .Select(i => (Id: i, Value: $"Value-{i}"))
            .ToDictionary(v => v.Id, v => v.Value);
        private ILogger<ValuesController> _Logger;

        public ValuesController(ILogger<ValuesController> Logger) => _Logger = Logger;

        [HttpGet] //ОБЯЗАТЕЛЬНО для каждого контроллера прописывать метод доступа к нему!!! MVC системы снова нет!!!
        public IEnumerable<string> GetAll() => _Values.Values;  //если не нужно возвращать статусные коды

        [HttpGet("{Id}")]   // Если есть параметр - описать его в методе доступа!! Соскучился уже по MVC?
        public IActionResult GetById(int Id)
        {
            //if (!_Values.ContainsKey(Id))
            //    return NotFound();

            //return Ok(_Values[Id]);

            if (_Values.TryGetValue(Id, out var value))
                return Ok(value);
            return NotFound();
        }
        [HttpGet("count")]
        public int Count() => _Values.Count;

        [HttpPost]              // POST -> http://localhost:5001/api/values 
        [HttpPost("add")]       // POST -> http://localhost:5001/api/values/add
        public IActionResult Add([FromBody] string Value)
        {
            var id = _Values.Count == 0 ? 1 : _Values.Keys.Max() + 1;
            _Values[id] = Value;
            return CreatedAtAction(nameof(GetById), new { id }, Value);
        }

        [HttpPut("Id")]         // PUT -> http://localhost:5001/api/values
        public IActionResult Edit(int Id, [FromBody] string Value)
        {
            if(!_Values.ContainsKey(Id))
                return NotFound();
            _Values[Id] = Value;
            return Ok();
        }

        [HttpDelete("{Id}")]    // DELETE -> http://localhost:5001/api/values/42
        public IActionResult Delete(int Id)
        {
            if (!_Values.ContainsKey(Id))
                return NotFound();
            _Values.Remove(Id);
            return Ok();
        }
    }
}
