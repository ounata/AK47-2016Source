﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SchemaList.aspx.cs" Inherits="AUCenter.SchemaAdmin.SchemaList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>列表</title>
    <link href="../Styles/pccom.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/aumain.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <au:SceneControl ID="SceneControl1" runat="server" />
    <div>
        <div class="pc-banner">
            <h1>
                <img src="../Images/icon_01.gif" alt="图标" />
                <span id="cateName" runat="server"></span>-<span>管理架构列表</span> <span class="pc-timepointmark">
                    <mcs:TimePointDisplayControl ID="TimePointDisplayControl1" runat="server" />
                </span>
            </h1>
        </div>
        <div class="pc-search-box-wrapper">
            <soa:DeluxeSearch ID="DeluxeSearch" runat="server" CssClass="deluxe-search deluxe-left"
                HasCategory="False" SearchFieldTemplate="CONTAINS(${DataField}$, ${Data}$)" SearchField="S.SearchContent"
                OnSearching="SearchButtonClick" OnConditionClick="onconditionClick" CustomSearchContainerControlID="advSearchPanel"
                HasAdvanced="true">
            </soa:DeluxeSearch>
            <soa:DataBindingControl runat="server" ID="searchBinding" AllowClientCollectData="True">
                <ItemBindings>
                    <soa:DataBindingItem ControlID="sfCodeName" DataPropertyName="CodeName" />
                </ItemBindings>
            </soa:DataBindingControl>
            <div id="advSearchPanel" runat="server" style="display: none" class="pc-search-advpan">
                <asp:HiddenField runat="server" ID="sfAdvanced" Value="False" />
                <table class="pc-search-grid-duo">
                    <tr>
                        <td>
                            <label for="sfCodeName" class="pc-label">
                                代码名称</label><asp:TextBox runat="server" ID="sfCodeName" MaxLength="56" CssClass="pc-textbox" />(精确)
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="pc-container5">
            <div class="pc-listmenu-container">
                <ul class="pc-listmenu" id="listMenu">
                    <li>
                        <asp:LinkButton runat="server" ID="lnkAdd" CssClass="button pc-list-cmd" OnClientClick="return ($pc.getEnabled(this)) && $pc.popups.newAdminSchema(this);"
                            OnClick="RefreshList">新建</asp:LinkButton>
                    </li>
                    <li>
                        <asp:LinkButton runat="server" ID="lnkDelete" CssClass="button pc-list-cmd" OnClick="RefreshList"
                            OnClientClick="return ($pc.getEnabled(this) && invokeBatchDelete() && false);">删除</asp:LinkButton>
                    </li>
                    <li class="pc-dropdownmenu"><span style="display: block; display: inline-block;">
                        <asp:LinkButton runat="server" CssClass="list-cmd" ID="btnImport" OnClientClick="return ($pc.getEnabled(this) && invokeImport());"
                            OnClick="RefreshList">导入</asp:LinkButton></span> </li>
                    <li class="pc-dropdownmenu"><span style="display: block; display: inline-block;">
                        <asp:LinkButton runat="server" CssClass="list-cmd" ID="LinkButton1" OnClientClick="return ($pc.getEnabled(this) && invokeExport() && false);"
                            OnClick="RefreshList">导出<i class="pc-arrow"></i></asp:LinkButton></span>
                        <div style="position: relative; z-index: 9">
                            <div style="position: absolute;">
                                <ul class="pc-popup-nav">
                                    <li>
                                        <asp:LinkButton runat="server" CssClass="list-cmd" ID="btnExportSelected" OnClientClick="return ($pc.getEnabled(this) && invokeExport() && false);"
                                            OnClick="RefreshList">导出选定架构（默认）</asp:LinkButton>
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" CssClass="list-cmd" ID="btnExportAll" OnClientClick="return ($pc.getEnabled(this) && invokeExportAll() && false);"
                                            OnClick="RefreshList">导出全部架构</asp:LinkButton>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </li>
                </ul>
            </div>
            <div style="display: none">
            </div>
            <div class="pc-grid-container">
                <mcs:DeluxeGrid ID="gridMain" runat="server" AutoGenerateColumns="False" DataSourceID="dataSourceMain"
                    AllowPaging="True" AllowSorting="True" ShowCheckBoxes="True" Category="PermissionCenter"
                    ShowExportControl="true" GridTitle="管理架构" DataKeyNames="ID" CssClass="dataList pc-datagrid"
                    TitleCssClass="title" PagerSettings-Position="Bottom" DataSourceMaxRow="0" TitleColor="141, 143, 149"
                    TitleFontSize="Large" OnRowCommand="gridMain_RowCommand">
                    <EmptyDataTemplate>
                        暂时没有您需要的数据
                    </EmptyDataTemplate>
                    <HeaderStyle CssClass="head" />
                    <Columns>
                        <asp:TemplateField HeaderText="名称" SortExpression="S.Name">
                            <ItemTemplate>
                                <div>
                                    <asp:HyperLink runat="server" ID="lnkName" CssClass="pc-item-link" Target="_blank"
                                        NavigateUrl='<%# Eval("ID","~/AdminUnitList.aspx?schemaId={0}") %>'><%# Server.HtmlEncode((string)Eval("Name")) %></asp:HyperLink>
                                </div>
                                <div id="Div1" class="pc-action-tray" runat="server" visible='<%# this.gridMain.ExportingDeluxeGrid == false %>'>
                                    <asp:LinkButton ID="lnkEdit1" runat="server" CssClass="pc-item-cmd" data-id='<%# System.Web.HttpUtility.HtmlAttributeEncode((string)Eval("ID"))%>'
                                        OnClientClick="return $pc.popups.editProperty(this);" OnClick="RefreshList">基本属性</asp:LinkButton>
                                    <asp:HyperLink ID="lnkSchemaRole" runat="server" CssClass="pc-item-cmd" Text="角色定义"
                                        NavigateUrl='<%#Eval("ID","~/SchemaAdmin/SchemaRoleList.aspx?id={0}") %>' onclick="return $pc.modalPopup(this);"
                                        Target="_blank"></asp:HyperLink>
                                    <asp:HyperLink ID="lnkPropertyExtension" runat="server" CssClass="pc-item-cmd" Text="单元扩展属性"
                                        NavigateUrl='<%#Eval("ID","~/Dialogs/PropertyEditor.aspx?auSchemaID={0}") %>' onclick="return $pc.modalPopup(this);"
                                        Target="_blank"></asp:HyperLink>
                                    <asp:LinkButton ID="lnkItemDelete" runat="server" Text="删除" CssClass="pc-item-cmd"
                                        Enabled='<%# this.EditEnabled %>' CommandName="DeleteItem" CommandArgument='<%#Eval("ID") %>'
                                        OnClientClick="return ($pc.getEnabled(this) && $pc.confirmDelete('确定要删除?'));"></asp:LinkButton>
                                    <asp:HyperLink ID="lnkHistory" runat="server" CssClass="pc-item-cmd" Target="_blank"
                                        onclick="return $pc.modalPopup(this);" NavigateUrl='<%#Eval("ID","~/ObjectHistoryLog.aspx?id={0}") %>'>历史</asp:HyperLink>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="代码名称" SortExpression="S.CodeName" DataField="CodeName"
                            HtmlEncode="true" />
                        <asp:BoundField HeaderText="显示名称" SortExpression="S.DisplayName" DataField="DisplayName"
                            HtmlEncode="true" />
                        <asp:TemplateField HeaderText="创建者" SortExpression="S.CreatorName">
                            <ItemTemplate>
                                <soa:UserPresence runat="server" ID="userPresence" ShowUserDisplayName="true" ShowUserIcon="false"
                                    UserID='<%#Eval("CreatorID") %>' UserDisplayName='<%# Server.HtmlEncode(Eval("CreatorName").ToString()) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CreateDate" HtmlEncode="true" HeaderText="创建日期" SortExpression="S.CreateDate"
                            DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
                    </Columns>
                    <PagerStyle CssClass="pager" />
                    <RowStyle CssClass="item" />
                    <CheckBoxTemplateItemStyle CssClass="checkbox" />
                    <AlternatingRowStyle CssClass="aitem" />
                    <PagerSettings FirstPageText="&lt;&lt;" LastPageText="&gt;&gt;" Mode="NextPreviousFirstLast"
                        NextPageText="下一页" Position="TopAndBottom" PreviousPageText="上一页"></PagerSettings>
                    <SelectedRowStyle CssClass="selecteditem" />
                    <CheckBoxTemplateHeaderStyle CssClass="checkbox" />
                    <PagerTemplate>
                    </PagerTemplate>
                </mcs:DeluxeGrid>
            </div>
        </div>
        <soa:DeluxeObjectDataSource runat="server" ID="dataSourceMain" TypeName="MCS.Library.SOA.DataObjects.Security.AUObjects.DataSources.AUSchemaDataSource"
            EnablePaging="True" OnSelecting="dataSourceMain_Selecting">
            <SelectParameters>
                <asp:QueryStringParameter Type="String" ConvertEmptyStringToNull="true" Name="categoryID"
                    QueryStringField="category" />
            </SelectParameters>
        </soa:DeluxeObjectDataSource>
        <div style="display: none">
            <asp:Button Text="Refresh" runat="server" ID="btnRefresh" OnClick="RefreshList" />
            <input type="hidden" runat="server" id="hfCategoryID" />
            <soa:UploadProgressControl runat="server" Category="管理单元" DialogHeaderText="导入管理架构"
                ID="ctlUpload" DialogTitle="导入管理架构" OnDoUploadProgress="DoFileUpload" OnLoadingDialogContent="ctlUpload_LoadingDialogContent"
                OnClientCompleted="postImportProcess" />
            <soa:PostProgressControl runat="server" ID="deleteMultiProgress" DialogTitle="批量删除"
                DialogHeaderText="删除进度..." OnClientCompleted="onDeleteCompleted" ControlIDToShowDialog="btnDeleteTrigger"
                OnClientBeforeStart="prepareDataForDelete" OnDoPostedData="DoDeleteProgress" />
        </div>
        <script type="text/javascript">

            $pc.ui.gridBehavior("gridMain", "hover");
            $pc.ui.listMenuBehavior("listMenu");

            function onconditionClick(sender, e) {
                var content = Sys.Serialization.JavaScriptSerializer.deserialize(e.ConditionContent);
                var bindingControl = $find("searchBinding");
                bindingControl.dataBind(content);
            }

            function invokeImport() {
                var result = $find("ctlUpload").showDialog()
                if (result)
                    return true;
                return false;
            }

            function postImportProcess(e) {
                if (e.dataChanged)
                    __doPostBack('btnImport', '');
            }

            function invokeExport() {
                var grid = $find("gridMain");
                if (grid) {
                    var keys = grid.get_clientSelectedKeys();
                    if (keys.length > 0) {
                        $pc.postViaIFrame($pc.appRoot + "Services/Export.ashx", { context: "SchemaList", id: keys });
                    }
                }
                grid = false;
                return false;
            }

            function invokeBatchDelete() {
                if ($find("gridMain").get_clientSelectedKeys().length) {
                    $find("deleteMultiProgress").showDialog();
                    return false;
                }
                return false;
            }

            function onDeleteCompleted() {
                $get("btnRefresh").click();
                return false;
            }

            function prepareDataForDelete(e) {
                e.steps = ($find("gridMain") || $find("grid2")).get_clientSelectedKeys();
                if (e.steps.length > 0) {
                    e.cancel = !confirm(e.steps.length === 1 ? "确实要删除这个项目？" : "确实要删除选定的项目？");
                }
            }

            function invokeExportAll() {
                $pc.postViaIFrame($pc.appRoot + "Services/Export.ashx", { context: "SchemaList", categoryId: $pc.get("hfCategoryID").value });
            }
        
        </script>
    </div>
    </form>
</body>
</html>
