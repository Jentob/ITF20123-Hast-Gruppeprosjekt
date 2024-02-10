﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIOF.Hast.HARB.Framework
{
    /// <summary>Represents cargo.</summary>
    public interface ICargo
    {
        public int Id { get; }
        public string Name { get; }
        public double WeightInKG { get; }
        public List<LogEntry> Log { get; }
		public void RecordHistory(LogEntry entry);
	}
}
