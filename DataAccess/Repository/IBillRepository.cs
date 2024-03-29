﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObject.Object;

namespace DataAccess.Repository
{
    public interface IBillRepository
    {
        List<Bill> GetBills();
        bool CreateNewReceipt(int idtable);
        bool CheckNoBill(int tableID);
        int GetLatestBill(int tableID);
        void SetSuspendedBill(int idbill);
        bool CheckBillStatus(int tableindex);
        int GetCountingBill(int tableID);
        void ConfirmCheckout(int idbill, int discount, decimal total);
    }
}
