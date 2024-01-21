using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects
{
	[Table("CreditScoreData")]
	public partial class CreditScoreData
	{
		[Key]
		public virtual int Id { get; set; }
		public virtual DateTime Date { get; set; }
		public virtual string CustomerId { get; set; }
		public virtual string Bureau { get; set; }
		public virtual string ScoreData { get; set; }
	}
}
