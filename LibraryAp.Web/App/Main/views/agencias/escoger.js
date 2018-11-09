(function () {
    angular.module('app').controller('app.views.agencias.escoger', [
        '$http', '$scope', '$uibModalInstance', 'abp.services.app.agencia',
        function ($http, $scope, $uibModalInstance, vService) {
            var vm = this;
            vm.agencias = [];
            vm.AgenciaId = 0;
            function getAgencias() {
                $http({
                    method: "POST",
                    url: "/Agencia/ListAll",
                    dataType: 'json',
                    headers: {
                        "Content-Type": "application/json"
                    }
                }).then(function (result) {
                    vm.agencias = result.data;
                });
            }

            vm.save = function () {
                $http({
                    method: "POST",
                    url: "/Agencia/Escoger",
                    dataType: 'json',
                    data: {
                        AgenciaId: vm.AgenciaId
                    },
                    headers: {
                        "Content-Type": "application/json"
                    }
                }).then(function (result) {
                    if (result.data.success) {
                        abp.notify.info(App.localize('SavedSuccessfully'));
                    } else {
                        abp.notify.error(App.localize('SavedError'));
                    }

                    $uibModalInstance.close();
                });
            };

            vm.cancel = function () {
                $uibModalInstance.dismiss({});
            };

            getAgencias();
        }
    ]);
})();