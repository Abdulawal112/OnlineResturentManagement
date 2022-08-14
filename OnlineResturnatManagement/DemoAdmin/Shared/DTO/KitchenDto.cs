﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineResturnatManagement.Shared.DTO
{
    public class KitchenDto: CreateUpdateDto
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string Name { get; set; }
        public string? ResponsiblePerson { get; set; }
        public string? Printer { get; set; }
        [NotMapped]
        public bool Editing { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;
    }
}
