// 這個方式主要是用在jQuery plugin，
// 實際上是執行了一個匿名的function並回傳jQuery物件，
// 當jQuery載入完成後便會開始執行，但無法操作DOM tree
(function ($) {
    $.extend(true,{
        // DataTable 基本設定
        dtBaseConfig: function () {
            // 把 ajax 錯誤訊息，從 alert 顯示改由 console 顯示
            $.fn.dataTable.ext.errMode = 'throw';
            return {
                // dom: "<'dt-warpper'ft<'col-xs-12 col-sm-6 no-padding'pl><'col-xs-12 col-sm-6 hidden-xs page-info'i>>",
                language: {
                    "emptyTable": "無資料...",
                    "processing": "處理中...",
                    "loadingRecords": "載入中...",
                    "lengthMenu": "顯示 _MENU_ 項結果",
                    "zeroRecords": "沒有符合的結果",
                    "info": "顯示第 _START_ 至 _END_ 項結果，共 _TOTAL_ 項",
                    "infoEmpty": "顯示第 0 至 0 項結果，共 0 項",
                    "infoFiltered": "(從 _MAX_ 項結果中過濾)",
                    "infoPostFix": "",
                    "search": "搜尋:",
                    "paginate": {
                        "first": "第一頁",
                        "previous": "上一頁",
                        "next": "下一頁",
                        "last": "最後一頁"
                    },
                    "aria": {
                        "sortAscending": ":升冪排列",
                        "sortDescending": ":降冪排列"
                    }
                },
                serverSide: true, // 啟用 ServerSide 模式
                searching: false, // 關閉 Filter 功能
                deferLoading: 0, // 畫面一載入時不發出 ajax 撈資料
                processing: true, // 讀取資料時，出現「Processing...」字眼，jQuery blockUI 也有 http://malsup.com/jquery/block/#page
                deferRender: true, // 延遲渲染用，假使有 1000 筆資料，DataTable 有分頁控制顯示，啟用後就不會一次建立 1000 筆 Html 元素，而是依照分頁顯示數來建立 Html 元素
                scrollX: true, // 資料欄位很多時，產生水平卷軸

                //scrollY: 250, // DataTables 高度
                scrollCollapse: false, // 設定為 True 可以調整垂直卷軸

                // 下拉選單的每頁顯示幾筆
                // [[顯示筆數的值],[顯示筆數的顯示字]]
                lengthMenu: [[1, 10, 20, 50, -1], [1, 10, 20, 50, "All"]],

                // 關閉 orderMulti 參數 ("orderMulti": false)，因為會增加排序處理複雜度、資料處理時間
                orderMulti: false
            };
        },

        // 建立 DataTable 的 Button 樣式設定
        dtCreateButton: function (btnConfig) {
            // 判斷有無給樣式，並給予基本的按鈕樣式
            if (!btnConfig.css) {
                btnConfig.css = "btn btn-default";
            } else {
                btnConfig.css += "btn btn-default";
            }
            // 判斷有無設定按鈕顯示文字，沒有就給 Button 文字
            if (!btnConfig.text) {
                btnConfig.text = "button";
            }

            switch (btnConfig.type) {
                case "edit":
                    btnConfig.iconCss = 'fa fa-edit';
                    btnConfig.text = "修改";
                    break;
                case "delete":
                    btnConfig.iconCss = 'fa fa-trash-o';
                    btnConfig.text = "刪除";
                    break;
                default:
                    btnConfig.iconCss = 'fa fa-book';
                    btnConfig.text = "button";
                    break;
            }

            btnConfig.textCss = "dt-btn-" + btnConfig.text;

            return "<button class ='" + btnConfig.css + " " + btnConfig.textCss + "'>" +
                "<i class='" + btnConfig.iconCss + "'></i>" +
                btnConfig.text + "</button>";
        },

        // 判斷回傳結果
        dtCheckRecords: function (json) {
            if (json.recordsTotal === 0) {
                $.infoBox('提示訊息', '所查詢的資料不存在，請重新輸入。');
            }
        }
    });
})(jQuery);