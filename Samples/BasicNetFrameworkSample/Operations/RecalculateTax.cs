﻿using CCP;
using CCP.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicNetCoreSample.Operations
{
    public class RecalculateTax : IOperation
    {
        [Required]
        public int YearFrom { get; set; }

        public int? YearTo { get; set; }

        public void Run()
        {
            Console.WriteLine($"Running RecalculateTax with YearFrom={YearFrom}, YearTo={YearTo}");
        }
    }
}