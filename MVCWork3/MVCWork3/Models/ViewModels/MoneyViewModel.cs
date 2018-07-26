using MVCWork3.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace MVCWork3.Models.ViewModels
{
    public class MoneyViewModel
    {
        public Guid Id { get; set; }

        //[Required(ErrorMessage = "請選擇類別")]
        public CategoryType Type { get; set; }

        [Required(ErrorMessage = "請輸入日期")]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}")]
        [DateValue(ErrorMessage ="日期不可大於今天")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "請填入金額")]
        [DisplayFormat(DataFormatString = "{0:n0}")]
        public int Price { get; set; }

        [Required(ErrorMessage = "請輸入備註")]
        public string Description { get; set; }
    }
}
