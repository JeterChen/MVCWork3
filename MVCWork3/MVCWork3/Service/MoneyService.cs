using MVCWork3.Enums;
using MVCWork3.Models;
using MVCWork3.Models.ViewModels;
using MVCWork3.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVCWork3.Service
{
    public class MoneyService : IMoneyService
    {

        private readonly IRepository<AccountBook> _accountBookRepository;

        public MoneyService(IUnitOfWork uok)
        {
            this._accountBookRepository = new Repository<AccountBook>(uok);
        }

        public IEnumerable<MoneyViewModel> LookupAllData()
        {
            var source = _accountBookRepository.LookupAll();

            var result = source.Select(a => new MoneyViewModel
            {
                Id = a.Id,
                Type = a.Categoryyy == 0 ? CategoryType.Expenditure : CategoryType.Income,
                Date = a.Dateee,
                Price = a.Amounttt
            });

            return result;
        }

        public IEnumerable<MoneyViewModel> LookupByPageList(int page, int pagesize)
        {
            //你根本沒用到他耶
            int currentPage = page < 1 ? 1 : page;

            var source = _accountBookRepository.LookupAll().OrderBy(b => b.Dateee).AsEnumerable();


            var pageResult = source.Select((item, inx) => new { item, inx })
                                   .GroupBy(x => x.inx / pagesize)
                                   .Select(g => g.Select(s => s.item));



            return pageResult.ToArray()[currentPage]
                             .OrderBy(o => o.Dateee)
                             .Select(s => new MoneyViewModel
                             {
                                 Id = s.Id,
                                 Type = s.Categoryyy == 0 ? CategoryType.Expenditure : CategoryType.Income,
                                 Date = s.Dateee,
                                 Price = s.Amounttt
                             });
        }

        public IEnumerable<MoneyViewModel> FakeLookupAllData()
        {
            var random = new Random();

            for (int i = 1; i <= 50; i++)
            {
                yield return new MoneyViewModel
                {
                    Type = random.Next(1, 100) % i == 0 ? CategoryType.Expenditure : CategoryType.Income,

                    Date = DateTime.Now.AddDays(-random.Next(0, 100)).AddMinutes(random.Next(0, 60)),

                    Price = random.Next(0, 10000)

                };
            }
        }

        public void Add(MoneyViewModel vo)
        {
            if (vo != null)
            {

                AccountBook accountBook = new AccountBook
                {
                    Id = Guid.NewGuid(),//既然你沒有要傳出去，你宣告變數幹啥？
                    Amounttt = vo.Price,
                    Categoryyy = vo.Type == CategoryType.Expenditure ? 0 : 1,
                    Dateee = vo.Date,
                    Remarkkk = vo.Description
                };

                _accountBookRepository.Create(accountBook);
            }
        }

        public void Edit(Guid id, MoneyViewModel vo)
        {
            if (vo != null)//這命名好爛.....
            {
                //你用 GetSingle 沒拿到就拋例外了，後面的檢查不會跑到
                var oldData = _accountBookRepository.GetSingle(a => a.Id == id);

                if (oldData != null)
                {
                    oldData.Amounttt = vo.Price;
                    oldData.Categoryyy = vo.Type == CategoryType.Expenditure ? 0 : 1;
                    oldData.Dateee = vo.Date;
                    oldData.Remarkkk = vo.Description;
                }
            }
        }

        public MoneyViewModel GetSingleData(Guid id)
        {
            return _accountBookRepository.Query((a) => a.Id == id)
                .Select((b) => new MoneyViewModel
                {
                    Id = b.Id,
                    Date = b.Dateee,
                    Type = (b.Categoryyy == 0 ? CategoryType.Expenditure : CategoryType.Income),
                    Description = b.Remarkkk,
                    Price = b.Amounttt
                }).FirstOrDefault();
        }

        public void Delete(Guid id)
        {
            var accountbook = _accountBookRepository.GetSingle((a) => a.Id == id);
            _accountBookRepository.Remove(accountbook);
        }
    }
}
