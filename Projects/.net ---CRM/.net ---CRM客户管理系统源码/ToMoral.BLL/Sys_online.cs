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
		/// ����һ������
		/// </summary>
		public void Add(tomoral.Model.Sys_online model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public bool Update(tomoral.Model.Sys_online model,string wherestr)
		{
			return dal.Update(model,wherestr);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public bool Delete(string wherestr)
		{
			//�ñ���������Ϣ�����Զ�������/�����ֶ�
            return dal.Delete(wherestr);
		}

		/// <summary>
		/// ��������б�
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// ���ǰ��������
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
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

