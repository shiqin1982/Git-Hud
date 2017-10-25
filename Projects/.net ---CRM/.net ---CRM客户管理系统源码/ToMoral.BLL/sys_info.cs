using System;
using System.Data;
using System.Collections.Generic;
using tomoral.Common;
using tomoral.Model;
namespace tomoral.BLL
{
	/// <summary>
	/// sys_info
	/// </summary>
	public partial class sys_info
	{
		private readonly tomoral.DAL.sys_info dal=new tomoral.DAL.sys_info();
		public sys_info()
		{}
		#region  Method
        
		/// <summary>
		/// ����һ������
		/// </summary>
		public bool Update(tomoral.Model.sys_info model)
		{
			return dal.Update(model);
		}		

		/// <summary>
		/// ��������б�
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}		

		/// <summary>
		/// ��������б�
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}
		

		#endregion  Method
	}
}

