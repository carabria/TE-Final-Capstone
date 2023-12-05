﻿namespace Capstone.Models
{
    public class Protein
    {
        public int ProteinId { get; set; }
        public string SequenceName { get; set; }
        public string ProteinSequence { get; set; }
        public string Description { get; set; }
        public int FormatType { get; set; }
        public int UserId { get; set; }
    }

    /// <summary>
    /// Model to accept a new Protein
    /// </summary>
    public class RegisterProtein
    {
        public string SequenceName { get; set; }
        public string ProteinSequence { get; set; }
        public string Description { get; set; }
        public int FormatType { get; set; }
        public int UserId { get; set; }
    }
}
