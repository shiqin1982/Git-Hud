using System;
using System.Data;
using System.Collections.Generic;
using tomoral.Common;
using tomoral.Model;
namespace tomoral.BLL
{
	/// <summary>
	/// tool_batch
	/// </summary>
	public partial class tool_batch
	{
		private readonly tomoral.DAL.tool_batch dal=new tomoral.DAL.tool_batch();
		public tool_batch()
		{}
		#region  Method
		

		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(tomoral.Model.tool_batch model)
		{
			return dal.Add(model);
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

		/// <summary>
		/// ��ҳ��ȡ�����б�
		/// </summary>
		public DataSet GetList(int PageSize, int PageIndex, string strWhere, string filedOrder, out string Total)
		{
			return dal.GetList(PageSize, PageIndex, strWhere, filedOrder, out Total);
		}

		#endregion  Method
	}
}

