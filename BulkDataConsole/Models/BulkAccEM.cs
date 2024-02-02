using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadDataProject.Models
{
    
    [Table("pubacc_em", Schema = "main")]
    [Index(nameof(UniqueSystemIdentifier))]
    public class BulkAccEM
    {
        
        [StringLength(2)]
        [Column(name: "record_type", Order = 0)]
        public required string RecordType { get; set; }

        [Precision(10, 0)] //Note this is 9 in legacy.
        [Column(name: "unique_system_identifier", Order = 1)]
        public required decimal UniqueSystemIdentifier { get; set; } = 0;

        [StringLength(14)]
        [Column(name: "uls_file_number", Order = 2)]
        public string? UlsFileNumber { get; set; }

        [MaxLength(30)]
        [Column(name: "ebf_number", Order = 3)]
        public string? EbfNumber { get; set; }

        [StringLength(10)]
        [Column(name: "call_sign", Order = 4)]
        public string? CallSign { get; set; }

        [Column(name: "location_number", Order = 5)]
        public int? LocationNumber { get; set; }

        [Column(name: "antenna_number", Order = 6)]
        public int? AntennaNumber { get; set; }

        [Precision(16, 8)]
        [Column(name: "frequency_assigned", Order = 7)]
        public decimal? FrequencyAssigned { get; set; }

        [StringLength(1)]
        [Column(name: "emission_action_performed", Order = 8)]
        public string? EmissionActionPerformed { get; set; }

        [StringLength(10)]
        [Column(name: "emission_code", Order = 9)]
        public string? EmissionCode { get; set; }

        [Precision(8, 1)]
        [Column(name: "digital_mod_rate", Order = 10)]
        public decimal? DigitalModRate { get; set; }

        [StringLength(255)]
        [Column(name: "digital_mod_type", Order = 11)]
        public string? DigitalModType { get; set; }

        [Column(name: "frequency_number", Order = 12)]
        public int? FrequencyNumber { get; set; }

        [StringLength(1)]
        [Column(name: "status_code", Order = 13)]
        public string? StatusCode { get; set; }

        [Column(name: "status_date", Order = 14)]
        public DateTime? StatusDate { get; set; }

        [Column(name: "emission_sequence_id", Order = 15)]
        public int? EmissionSequenceId { get; set; }
    }
}
