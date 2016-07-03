define([ppts.config.modules.order], function (helper) {

    helper.registerFactory('purchaseCourseDataService', ['$resource', function ($resource, dataService) {

        var resource = $resource(ppts.config.orderApiBaseUrl + 'api/purchase/:operation/:id',
            { operation: '@operation' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false }
            });


        //-----------------------订购单列表--------------------------
        resource.getAllOrderItems = function (criteria, callback) {
            resource.post({ operation: 'GetAllOrderItems' }, criteria, function (entity) { if (callback) { callback(entity); } });
        }
        resource.getPagedOrderItems = function (criteria, callback) {
            resource.post({ operation: 'GetPagedProducts' }, criteria, function (entity) { if (callback) { callback(entity); } });
        }
        resource.getOrderItem = function (id, callback) {
            resource.post({ operation: 'GetOrderItemView' }, { id: id }, function (entity) { if (callback) { callback(entity); } });
        };



        //---------------------------------------------------------


        //-----------------------常规订购--------------------------
        //使用 product.service
        //---------------------------------------------------------


        //------------------------班组订购-------------------------
        resource.getClassGroupProductList = function (criteria, callback) {
            resource.post({ operation: 'GetClassGroupProductList' }, criteria, function (entity) { if (callback) { callback(entity); } });
        }
        resource.getPagedClassGroupProducts = function (criteria, callback) {
            resource.post({ operation: 'GetPagedClassGroupProducts' }, criteria, function (entity) { if (callback) { callback(entity); } });
        }

        resource.getPagedClasses = function (criteria, callback) {
            resource.post({ operation: 'GetPagedClasses' }, criteria, function (entity) { if (callback) { callback(entity); } });
        }

        //---------------------------------------------------------

        //------------------------订购清单-------------------------

        resource.getShoppingCart = function (data, callback) {
            resource.post({ operation: 'GetShoppingCart' }, data, function (entity) { if (callback) { callback(entity); } });
        }

        resource.deleteShoppingCart = function (cartIDs, callback) {
            resource.post({ operation: 'DeleteShoppingCart' }, cartIDs, function (entity) { if (callback) { callback(entity); } });
        }

        resource.addShoppingCart = function (data, callback) {
            resource.post({ operation: 'AddShoppingCart' }, data, function (entity) { if (callback) { callback(entity); } });
        }

        resource.submitShoppingCart = function (data, callback) {
            resource.post({ operation: 'SubmitShoppingCart' }, data, function (entity) { if (callback) { callback(entity); } });
        }

        //获取要扣取的服务费
        resource.getServiceCharge = function (data, callback) {
            //{ customerId: customerId, campusId: campusId }
            resource.post({ operation: 'GetServiceChargeByUserId' }, data, function (entity) { if (callback) { callback(entity); } });
        }

        //---------------------------------------------------------

        //------------------------查看订购清单-------------------------

        resource.getOrder = function (data, callback) {
            resource.post({ operation: 'GetOrder' }, data, function (entity) { if (callback) { callback(entity); } });
        }



        //---------------------------------------------------------


        //------------------------资产兑换-------------------------


        resource.getExchangeInfo = function (data, callback) {
            resource.post({ operation: 'GetExchangeInfo' }, data, function (entity) { if (callback) { callback(entity); } });
        }

        resource.exchangeOrder = function (data, callback) {
            resource.post({ operation: 'ExchangeOrder' }, data, function (entity) { if (callback) { callback(entity); } });
        }

        resource.getProduct = function (id, callback) {
            resource.post({ operation: 'GetProductByID' }, { productId: id }, callback);
        }

        //------------------------------------------------------------------

        //------------------------编辑缴费单-------------------------

        resource.getEditPayment = function (data, callback) {
            resource.post({ operation: 'GetEditPayment' }, data, function (entity) { if (callback) { callback(entity); } });
        }

        resource.editPayment = function (data, callback) {
            resource.post({ operation: 'EditPayment' }, data, function (entity) { if (callback) { callback(entity); } });
        }

        //------------------------------------------------------------------



        //------------------------订购历史-------------------------

        resource.getRecognizingIncome = function (itemID, callback) {
            resource.query({ operation: 'GetRecognizingIncomeList', itemID: itemID }, callback);
        }
        resource.submitRecognizingIncome = function (data, callback) {
            resource.post({ operation: 'AddRecognizingIncome' }, data, callback);
        }
        //

        //------------------------------------------------------------------

        resource.getAssetConsumeViews = function (criteria, success, error) {
            resource.post({ operation: 'getAssetConsumeViews' }, criteria, success, error);
        }

        resource.getPageAssetConsumeViews = function (criteria, success, error) {
            resource.post({ operation: 'getPageAssetConsumeViews' }, criteria, success, error);
        }

        //------------------------审批-------------------------

        resource.getOrderByWorkflow = function (data, callback) {
            resource.post({ operation: 'GetOrderByWorkflow' }, data, callback);
        }
        //---------------------------------------------------------
        return resource;
    }]);



});