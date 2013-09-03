﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.ComponentModel.Composition;
using System.Windows;

namespace OfficeFinancialUI.ViewModel
{
	public class BaseModel 
	{
		private static BaseModel _instance;
		public static BaseModel Instance
		{
			get
			{
				if (_instance == null)
					_instance = new BaseModel();
				return _instance;
			}
		}

		private  ImportModule _importCatalog;
		public ImportModule ImportCatalog
		{
			get
			{
				_importCatalog = _importCatalog ?? new ImportModule();
				return _importCatalog;
			}
		}

		
	}
}
