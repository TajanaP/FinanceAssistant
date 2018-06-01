using AutoMapper;
using FinanceAssistant.Controllers.ViewModels;
using FinanceAssistant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceAssistant.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain To ViewModel
            CreateMap<Transaction, TransactionViewModel>();
            CreateMap<Transaction, SaveTransactionViewModel>();
            CreateMap<TransactionType, TransactionTypeViewModel>();
            CreateMap<TransactionCategory, TransactionCategoryViewModel>();
            CreateMap<TransactionCategory, SaveTransactionCategoryViewModel>();

            // ViewModel To Domain
            CreateMap<SaveTransactionCategoryViewModel, TransactionCategory>();
            CreateMap<SaveTransactionViewModel, Transaction>();
        }
    }
}
