namespace RiderQc.Web.DAL.Entity
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Moto")]
    public partial class Moto
    {
        public int MotoId { get; set; }

        public int UserId { get; set; }

        [Required]
        [StringLength(25)]
        public string Brand { get; set; }

        [StringLength(25)]
        public string Model { get; set; }

        public int? Year { get; set; }

        public int? Type { get; set; }

        public virtual User User { get; set; }
    }
}
