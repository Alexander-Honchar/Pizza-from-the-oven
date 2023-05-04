﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Пицца_Сайт.Models.DTO
{
    public class PizzaDTO
    {
        public uint Id { get; set; }


		public string? Name { get; set; }


		public double Price { get; set; }


		[Required]
        public uint SizePizzaId { get; set; }
        [ForeignKey("SizePizzaId")]
        public SizePizzaDTO? SizePizza { get; set; }



        [Required]
        public uint CategoryPizzaId { get; set; }
        [ForeignKey("CategoryId")]
        public CategoryPizzaDTO? CategoryPizza { get; set; }
    }
}
