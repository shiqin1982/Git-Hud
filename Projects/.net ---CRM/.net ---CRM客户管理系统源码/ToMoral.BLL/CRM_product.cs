using System;
using System.Data;
using System.Collections.Generic;
using tomoral.Common;
using tomoral.Model;
namespace tomoral.BLL
{
	/// <summary>
	/// CRM_product
	/// </summary>
	public partial class CRM_product
	{
		private readonly tomoral.DAL.CRM_product dal=new tomoral.DAL.CRM_product();
		public CRM_product()
		{}
		#region  Method

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(int product_id)
		{
			return dal.Exists(product_id);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(tomoral.Model.CRM_product model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public bool Update(tomoral.Model.CRM_product model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// Ԥɾ��
		/// </summary>
		/// <param name="id"></param>
		/// <param name="isDelete"></param>
		/// <param name="time"></param>
		/// <returns></returns>
		public bool AdvanceDelete(int id, int isDelete, string time)
		{
			return dal.AdvanceDelete(id, isDelete, time);
		}
		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public bool Delete(int product_id)
		{
			
			return dal.Delete(product_id);
		}
		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public bool DeleteList(string product_idlist )
		{
			return dal.DeleteList(product_idlist );
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public tomoral.Model.CRM_product GetModel(int product_id)
		{
			
			return dal.GetModel(product_id);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ�����
		/// </summary>
		public tomoral.Model.CRM_product GetModelByCache(int product_id)
		{
			
			string CacheKey = "CRM_productModel-" + product_id;
			object objModel = tomoral.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(product_id);
					if (objModel != null)
					{
						int ModelCache = tomoral.Common.ConfigHelper.GetConfigInt("ModelCache");
						tomoral.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (tomoral.Model.CRM_product)objModel;
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
		public List<tomoral.Model.CRM_product> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<tomoral.Model.CRM_product> DataTableToList(DataTable dt)
		{
			List<tomoral.Model.CRM_product> modelList = new List<tomoral.Model.CRM_product>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				tomoral.Model.CRM_product model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new tomoral.Model.CRM_product();
					if(dt.Rows[n]["product_id"]!=null && dt.Rows[n]["product_id"].ToString()!="")
					{
						model.product_id=int.Parse(dt.Rows[n]["product_id"].ToString());
					}
					if(dt.Rows[n]["product_name"]!=null && dt.Rows[n]["product_name"].ToString()!="")
					{
					model.product_name=dt.Rows[n]["product_name"].ToString();
					}
					if(dt.Rows[n]["category_id"]!=null && dt.Rows[n]["category_id"].ToString()!="")
					{
						model.category_id=int.Parse(dt.Rows[n]["category_id"].ToString());
					}
					if(dt.Rows[n]["category_name"]!=null && dt.Rows[n]["category_name"].ToString()!="")
					{
					model.category_name=dt.Rows[n]["category_name"].ToString();
					}
					if(dt.Rows[n]["specifications"]!=null && dt.Rows[n]["specifications"].ToString()!="")
					{
					model.specifications=dt.Rows[n]["specifications"].ToString();
					}
					if(dt.Rows[n]["status"]!=null && dt.Rows[n]["status"].ToString()!="")
					{
					model.status=dt.Rows[n]["status"].ToString();
					}
					if(dt.Rows[n]["unit"]!=null && dt.Rows[n]["unit"].ToString()!="")
					{
					model.unit=dt.Rows[n]["unit"].ToString();
					}
					if(dt.Rows[n]["remarks"]!=null && dt.Rows[n]["remarks"].ToString()!="")
					{
					model.remarks=dt.Rows[n]["remarks"].ToString();
					}
					if(dt.Rows[n]["price"]!=null && dt.Rows[n]["price"].ToString()!="")
					{
						model.price=decimal.Parse(dt.Rows[n]["price"].ToString());
					}
					if(dt.Rows[n]["isDelete"]!=null && dt.Rows[n]["isDelete"].ToString()!="")
					{
						model.isDelete=int.Parse(dt.Rows[n]["isDelete"].ToString());
					}
					if(dt.Rows[n]["Delete_time"]!=null && dt.Rows[n]["Delete_time"].ToString()!="")
					{
						model.Delete_time=DateTime.Parse(dt.Rows[n]["Delete_time"].ToString());
					}
					modelList.Add(model);
				}
			}
			return modelList;
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

