// This file was automatically generated by the Dapper.SimpleCRUD T4 Template
// Do not make changes directly to this file - edit the template instead
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: `Cardconnect`
//     Provider:               `System.Data.SqlClient`
//     Connection String:      `Data Source=.;Initial Catalog=PaystackCards;Integrated Security=True`
//     Include Views:          `True`

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BANKSOFRI_LOAN.DATALAYER.DomainObjects.Models
{
    /// <summary>
    /// A class which represents the BankCodes table.
    /// </summary>
	[Table("BankCodes")]
	public partial class BankCode
	{
		public virtual int Id { get; set; }
		public virtual string Code { get; set; }
		public virtual string Gateway { get; set; }
		public virtual string Name { get; set; }
		public virtual string Status { get; set; }
	}

    /// <summary>
    /// A class which represents the Income_Data table.
    /// </summary>
	[Table("Income_Data")]
	public partial class IncomeData
	{
		[Key]
		public virtual string IPPIS_Num { get; set; }
		public virtual string Firstname { get; set; }
		public virtual string Surname { get; set; }
		public virtual string MDA { get; set; }
		public virtual decimal Monthly_Income { get; set; }
	}

    /// <summary>
    /// A class which represents the Authorised table.
    /// </summary>
	[Table("Authorised")]
	public partial class Authorised
	{
		[Key]
		public virtual int Id { get; set; }
		public virtual DateTime Date { get; set; }
		public virtual string UserName { get; set; }
		public virtual string AppId { get; set; }
		public virtual string AppKey { get; set; }
	}

    /// <summary>
    /// A class which represents the CardTransactions table.
    /// </summary>
	[Table("CardTransactions")]
	public partial class CardTransaction
	{
		[Key]
		public virtual int Id { get; set; }
		public virtual string message { get; set; }
		public virtual string reference { get; set; }
		public virtual string status { get; set; }
		public virtual string trans { get; set; }
		public virtual string transactions { get; set; }
		public virtual string email { get; set; }
		public virtual string AuthorisationCode { get; set; }
		public virtual string CardBin { get; set; }
		public virtual DateTime DateCreated { get; set; }
	}

    /// <summary>
    /// A class which represents the Logs table.
    /// </summary>
	[Table("Logs")]
	public partial class Log
	{
		[Key]
		public virtual int Id { get; set; }
		public virtual DateTime LogTime { get; set; }
		public virtual string Type { get; set; }
		public virtual string Details { get; set; }
	}

    /// <summary>
    /// A class which represents the ChargeHistory table.
    /// </summary>
	[Table("ChargeHistory")]
	public partial class ChargeHistory
	{
		[Key]
		public virtual int Id { get; set; }
		public virtual DateTime Date { get; set; }
		public virtual string Email { get; set; }
		public virtual string ReferenceId { get; set; }
		public virtual decimal AmountCharge { get; set; }
		public virtual string ChargedBy { get; set; }
		public virtual string Status { get; set; }
	}

    /// <summary>
    /// A class which represents the ChargeAttempts table.
    /// </summary>
	[Table("ChargeAttempts")]
	public partial class ChargeAttempt
	{
		[Key]
		public virtual int Id { get; set; }
		public virtual DateTime Date { get; set; }
		public virtual string Email { get; set; }
		public virtual string ReferenceId { get; set; }
		public virtual decimal ChargeAmount { get; set; }
		public virtual string ChargedBy { get; set; }
		public virtual string Status { get; set; }
	}

    /// <summary>
    /// A class which represents the CustomerInvite table.
    /// </summary>
	[Table("CustomerInvite")]
	public partial class CustomerInvite
	{
		[Key]
		public virtual int Id { get; set; }
		public virtual DateTime Date { get; set; }
		public virtual string CustomerName { get; set; }
		public virtual string Email { get; set; }
		public virtual string InvitedBy { get; set; }
		public virtual string InviteStatus { get; set; }
	}

}