using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendProyectoFinal.Models
{
    public class PaymentDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentDetailID { get; set; }
        public string CardHolderName { get; set; }
        public string LastFourDigits { get; set; }
        public string CardType { get; set; }
        public string ExpirationDate { get; set; }
        public int PaymentID { get; set; }
        [ForeignKey("PaymentID")]
        public virtual Payment Payment { get; set; }
    }
}
