﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardSystemMng.Model.Models
{
    public class RootEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }


    }
}