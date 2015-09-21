using System.Collections.Generic;
using System.Data;

namespace DotNet.Utilities
{
    public static class JqGridHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="pidName"></param>
        /// <param name="pidText"></param>
        /// <param name="idName"></param>
        /// <param name="nameText"></param>
        /// <param name="img"></param>
        /// <param name="navigation"></param>
        /// <param name="setCheck">判断是否被选中</param>
        /// <returns></returns>
        public static List<TreeView> BindModuleTreeView(DataTable dt, string pidName, string pidText, string idName, string nameText, string img = null, string navigation = null, bool setCheck = false, bool isNotShowParentCheck = false)
        {
            List<TreeView> list = new List<TreeView>();

            DataView dv = new DataView(dt);
            TreeView tn;
            string filter = string.Format(pidName + "='{0}'", pidText);
            dv.RowFilter = filter;
            foreach (DataRowView drv in dv)
            {
                string id = drv[idName].ToString();
                tn = new TreeView//建立一个新节点
                {
                    id = id,//节点的ID
                    value=id,
                    text = drv[nameText].ToString(),//节点的Text，节点的文本显示
                    img = img != null ? "/Content/Images/Icon16/" + drv[img].ToString() : string.Empty,
                    Location = navigation != null ? drv[navigation].ToString() : string.Empty,
                    parentnodes = drv[pidName].ToString(),
                    isexpand = true,
                    complete = true
                };

                if (setCheck)
                {
                    tn.checkstate = drv["checkstate"].ToString();
                }

                List<TreeView> list2 = BindModuleTreeView(dt, pidName, id, idName, nameText, img, navigation, setCheck, isNotShowParentCheck);
                if (list2.Count > 0)
                {
                    tn.hasChildren = true;
                    tn.ChildNodes = list2;
                    if (isNotShowParentCheck)
                    {
                        tn.showcheck = false;
                        
                    }
                    //tn.cascadecheck = false;
                }

                list.Add(tn);
            }
            return list;
        }
         public static List<TreeView> BindOrganizationTreeView(DataTable dt, string pidName, string pidText, string idName, string nameText, string img = null, string navigation = null)
        {
            List<TreeView> list = new List<TreeView>();

            DataView dv = new DataView(dt);
            TreeView tn;
            string filter = string.Format(pidName + "='{0}'", pidText);
            dv.RowFilter = filter;
            foreach (DataRowView drv in dv)
            {
                string id = drv[idName].ToString();

                string imgTmp = string.Empty;
                switch (drv["category"].ToString())
                {
                    case "0"://公司
                        imgTmp = "molecule.png";
                        break;
                    case "1"://部门
                        imgTmp = "hostname.png";

                        break;
                }
                tn = new TreeView//建立一个新节点
                {
                    id = id,//节点的ID
                    text = drv[nameText].ToString(),//节点的Text，节点的文本显示
                    img = imgTmp != string.Empty ? "/Content/Images/Icon16/" + imgTmp : imgTmp,
                    Location = navigation != null ? drv[navigation].ToString() : string.Empty,
                    parentnodes = drv[pidName].ToString(),
                    isexpand = true,
                    complete = true
                };

                List<TreeView> list2 = BindOrganizationTreeView(dt, pidName, id, idName, nameText, img, navigation);
                if (list2.Count > 0)
                {
                    tn.hasChildren = true;
                    tn.ChildNodes = list2;
                }

                list.Add(tn);
            }
            return list;
        }


         public static List<TreeView> BindModuleAndButtonTreeView(DataTable dtModule,DataTable dtButton, string pidName, string pidText, string idName, string nameText, string img = null)
         {
             List<TreeView> list = new List<TreeView>();

             DataView dv = new DataView(dtModule);
             TreeView tn;
             string filter = string.Format(pidName + "='{0}'", pidText);
             dv.RowFilter = filter;
             foreach (DataRowView drv in dv)
             {
                 string id = drv[idName].ToString();
                 tn = new TreeView//建立一个新节点
                 {
                     id = id,//节点的ID
                     value = id,
                     text = drv[nameText].ToString(),//节点的Text，节点的文本显示
                     img = img != null ? "/Content/Images/Icon16/" + drv[img].ToString() : string.Empty,
                     parentnodes = drv[pidName].ToString(),
                     isexpand = true,
                     complete = true
                 };


                 List<TreeView> list2 = BindModuleAndButtonTreeView(dtModule,dtButton, pidName, id, idName, nameText, img);
                 if (list2.Count > 0)
                 {
                     tn.hasChildren = true;
                     tn.ChildNodes = list2;
                 }
                 else
                 {
                     string filterButton = string.Format("moduleid" + "='{0}'", id);
                     DataView dvButton = new DataView(dtButton);

                     dvButton.RowFilter = filterButton;
                     if (dvButton.Count>0)
                     {
                         List<TreeView> listButton = new List<TreeView>();
                         foreach (DataRowView drvButton in dvButton)
                         {

                             string category = drvButton["category"].ToString() == "1" ? "工具栏" : "右键";
                             TreeView tnButton = new TreeView//建立一个新节点
                             {
                                 id = drvButton["ButtonId"].ToString(),
                                 value = drvButton["ButtonId"].ToString(),

                                 text = drvButton["FullName"].ToString() + "(" + category + ")",//节点的Text，节点的文本显示
                                 img = img != null ? "/Content/Images/Icon16/" + drvButton["Icon"].ToString() : string.Empty,
                                 parentnodes = id,
                                 isexpand = true,
                                 complete = true
                             };


                             listButton.Add(tnButton);
                         }

                         tn.hasChildren = true;
                         tn.ChildNodes = listButton;
                     }
                 }

                 list.Add(tn);
             }
             return list;
         }


    }
}
