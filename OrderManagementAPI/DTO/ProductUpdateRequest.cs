﻿namespace OrderManagementAPI.DTO
{
    public class ProductUpdateRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string CategoryId { get; set; }
    }
}