﻿using CES.CoreApi.Receipt_Main.Application.Core;
using CES.CoreApi.Receipt_Main.Domain.Core.Repositories;
using CES.CoreApi.Receipt_Main.Domain.Core.Services;
using CES.CoreApi.Receipt_Main.Infrastructure.Data;
using CES.CoreApi.Receipt_Main.Infrastructure.Data.Repository;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.UI.WPF
{
    public class MyModule : NinjectModule
    {
        public override void Load()
        {         
            Bind<IUserService>().To<UserService>();  
            Bind<DbContext>().To<ReceiptDbContext>();

            Bind<IDocumentService>().To<DocumentService>();
            Bind<IDocumentRepository>().To<DocumentRepository>();

            Bind<ITaxEntityService>().To<TaxEntityService>();
            Bind<ITaxEntityRepository>().To<TaxEntityRepository>();

            Bind<ITaxAddressService>().To<TaxAddressService>();
            Bind<ITaxAddressRepository>().To<TaxAddressRepository>();

            Bind<ISequenceService>().To<SequenceService>();
            Bind<ISequenceRepository>().To<SequenceRepository>();

            Bind<IStoreService>().To<StoreService>();
            Bind<IStoreRepository>().To<StoreRepository>();

            Bind<IDialogService>().To<DialogService>();          

        }
    }
}