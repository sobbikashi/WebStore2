﻿

namespace WebStore.Interfaces;

public static class WebAPIAddresses
{
    public class V1
    {
        public const string Employees = "api/v1/v1/employees";
        public const string Orders = "api/v1/orders";
        public const string Products = "api/v1/products";
        public const string Values = "api/v1/valuess";

        public static class Identity
        {
            public const string Users = "api/v1/identity/users";
            public const string Roles = "api/v1/identity/roles";
        }
    }
        
    


}
