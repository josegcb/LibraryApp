(function () {
    var controllerId = 'app.views.layout';
    angular.module('app').controller(controllerId, [
        '$scope', '$timeout', '$window', function ($scope, $timeout, $window) {
            var vm = this;
            //Layout logic...


            vm.activateLeftSideBar = function () {
                $timeout(function () {
                    $.AdminBSB.leftSideBar.activate();
                }, 2000);
            };

            vm.activateRightSideBar = function () {
                $timeout(function () {
                    $.AdminBSB.rightSideBar.activate();
                }, 2000);
            };


            vm.activateTopBar = function () {
                $.AdminBSB.search.activate();
                $.AdminBSB.navbar.activate();
            };

            $window.onbeforeunload = function () {
                $window.localStorage["key"] = "value";
                localStorage.removeItem("key");
                return null;
            };

        }]);

   

})();

