﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NeteaseCloudMusic.NET.Models
{
    public class NeteaseRequestData
    {

        [JsonPropertyName("csrf")]
        public string Csrf { get; set; } = "";
    }
}
