using System;
using System.Data;
using System.Collections.Generic;
using tomoral.Common;
using tomoral.Model;
namespace tomoral.BLL
{
	/// <summary>
	/// public_news
	/// </summary>
	public partial class public_news
	{
		private readonly tomoral.DAL.public_news dal=new tomoral.DAL.public_news();
		public public_news()
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
		public bool Exists(int id)
		{
			return dal.Exists(id);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(tomoral.Model.public_news model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public bool Update(tomoral.Model.public_news model)
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
		public bool Delete(int id)
		{
			
			return dal.Delete(id);
		}
		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public bool DeleteList(string idlist )
		{
			return dal.DeleteList(idlist );
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public tomoral.Model.public_news GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ�����
		/// </summary>
		public tomoral.Model.public_news GetModelByCache(int id)
		{
			
			string CacheKey = "public_newsModel-" + id;
			object objModel = tomoral.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(id);
					if (objModel != null)
					{
						int ModelCache = tomoral.Common.ConfigHelper.GetConfigInt("ModelCache");
						tomoral.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (tomoral.Model.public_news)objModel;
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
		public List<tomoral.Model.public_news> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<tomoral.Model.public_news> DataTableToList(DataTable dt)
		{
			List<tomoral.Model.public_news> modelList = new List<tomoral.Model.public_news>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				tomoral.Model.public_news model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new tomoral.Model.public_news();
					if(dt.Rows[n]["id"]!=null && dt.Rows[n]["id"].ToString()!="")
					{
						model.id=int.Parse(dt.Rows[n]["id"].ToString());
					}
					if(dt.Rows[n]["news_title"]!=null && dt.Rows[n]["news_title"].ToString()!="")
					{
					model.news_title=dt.Rows[n]["news_title"].ToString();
					}
					if(dt.Rows[n]["news_content"]!=null && dt.Rows[n]["news_content"].ToString()!="")
					{
					model.news_content=dt.Rows[n]["news_content"].ToString();
					}
					if(dt.Rows[n]["create_id"]!=null && dt.Rows[n]["create_id"].ToString()!="")
					{
						model.create_id=int.Parse(dt.Rows[n]["create_id"].ToString());
					}
					if(dt.Rows[n]["create_name"]!=null && dt.Rows[n]["create_name"].ToString()!="")
					{
					model.create_name=dt.Rows[n]["create_name"].ToString();
					}
					if(dt.Rows[n]["dep_id"]!=null && dt.Rows[n]["dep_id"].ToString()!="")
					{
						model.dep_id=int.Parse(dt.Rows[n]["dep_id"].ToString());
					}
					if(dt.Rows[n]["dep_name"]!=null && dt.Rows[n]["dep_name"].ToString()!="")
					{
					model.dep_name=dt.Rows[n]["dep_name"].ToString();
					}
					if(dt.Rows[n]["news_time"]!=null && dt.Rows[n]["news_time"].ToString()!="")
					{
						model.news_time=DateTime.Parse(dt.Rows[n]["news_time"].ToString());
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

