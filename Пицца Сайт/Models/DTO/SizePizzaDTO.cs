﻿using System.ComponentModel.DataAnnotations;


namespace Пицца_Сайт.Models.DTO
{
    public class SizePizzaDTO
    {
        public uint Id { get; set; }

        [Required]
        public string? Size { get; set; }


    }
}
