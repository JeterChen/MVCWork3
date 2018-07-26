using MVCWork3.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace MVCWork3.Service
{
    public interface IMoneyService
    {
        /// <summary>
        /// 取得所有AccountBook資料
        /// </summary>
        /// <returns></returns>
        IEnumerable<MoneyViewModel> LookupAllData();

        /// <summary>
        /// byPage取得所有AccountBook資料
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        IEnumerable<MoneyViewModel> LookupByPageList(int page, int pagesize);

        /// <summary>
        /// 取得所有FakeData
        /// </summary>
        /// <returns></returns>
        IEnumerable<MoneyViewModel> FakeLookupAllData();

        /// <summary>
        /// 新增資料至AccountBook
        /// </summary>
        /// <param name="vo">ViewModelData</param>
        void Add(MoneyViewModel vo);

        /// <summary>
        /// 修改AccountBook資料
        /// </summary>
        /// <param name="id">Key</param>
        /// <param name="vo">ViewModelData</param>
        void Edit(Guid id, MoneyViewModel vo);

        /// <summary>
        /// 取得一筆資料
        /// </summary>
        /// <param name="id">Key</param>
        /// <returns>MoneyViewModel</returns>
        MoneyViewModel GetSingleData(Guid id);

        /// <summary>
        /// 刪除一筆資料
        /// </summary>
        /// <param name="id">KEY</param>
        void Delete(Guid id);

    }
}
