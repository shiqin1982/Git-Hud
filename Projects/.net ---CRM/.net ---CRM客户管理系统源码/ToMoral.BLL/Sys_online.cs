using System;
using System.Data;
using System.Collections.Generic;
using tomoral.Common;
using tomoral.Model;
namespace tomoral.BLL
{
	/// <summary>
	/// Sys_online
	/// </summary>
	public partial class Sys_online
	{
		private readonly tomoral.DAL.Sys_online dal=new tomoral.DAL.Sys_online();
		public Sys_online()
		{}
		#region  Method

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(tomoral.Model.Sys_online model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(tomoral.Model.Sys_online model,string wherestr)
		{
			return dal.Update(model,wherestr);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string wherestr)
		{
			//该表无主键信息，请自定义主键/条件字段
            return dal.Delete(wherestr);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}


		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}



		#endregion  Method
	}
}

