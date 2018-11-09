(function () {
    angular.module('app')
        .controller('app.views.agencias.input', [
            '$http', '$scope', '$uibModalInstance', 'abp.services.app.agencia', 'parameters',
            function ($http, $scope, $uibModalInstance, vService, parameters) {
                var vm = this;
                var descripcionesStatus = [];
                var descripcionesComisionPor = [];
                $scope.action = parameters.action;
                var idRecord = parameters.idRecord;
                vm.enabled = false;
                vm.data = {
                    codigo: '',
                    nombre: '',
                    direccion: '',
                    telefono: '',
                    statusRegistro: 0,
                    comisionPor: 0,
                    porcentajePorPremio: 0,
                    comentario: ''
                }
                vm.save = function () {
                    switch ($scope.action) {
                        case "Insertar":
                            vService.create(vm.data)
                                .then(function () {
                                    abp.notify.info(App.localize('SavedSuccessfully'));
                                    $uibModalInstance.close();
                                });
                            break;
                        case "Modificar":
                            vService.update(vm.data)
                                .then(function () {
                                    abp.notify.info(App.localize('SavedSuccessfully'));
                                    $uibModalInstance.close();
                                });
                            break;
                        case "Eliminar":
                            abp.message.confirm("Desea eliminar el registro?",
                                function (result) {
                                    if (result) {
                                        vService.delete({
                                            id: idRecord,
                                            timeStamp: vm.data.timeStamp
                                        })
                                            .then(function () {
                                                abp.notify.info("Registro eliminado ");
                                                $uibModalInstance.close();
                                            });
                                    }
                                });
                            break;
                        case "Consultar":
                            break;
                        default:
                    }
                };

                vm.cancel = function () {
                    $uibModalInstance.dismiss({});
                };

                getDescripcionesStatus = function () {
                    $http({
                        method: "POST",
                        url: "/Agencia/DescripcionesStatus",
                        dataType: 'json',
                        headers: {
                            "Content-Type": "application/json"
                        }
                    }).then(function (result) {
                        vm.descripcionesStatus = result.data;
                    });
                }

                getDescripcionesComisionPor = function () {
                    $http({
                        method: "POST",
                        url: "/Agencia/DescripcionesComisionPor",
                        dataType: 'json',
                        headers: {
                            "Content-Type": "application/json"
                        }
                    }).then(function (result) {
                        vm.descripcionesComisionPor = result.data;
                    });
                }

                inicializar = function () {
                    if ($scope.action != "Insertar") {
                        vService.getById({
                            id: idRecord
                        })
                            .then(function (result) {
                                vm.data = result.data;
                            });
                        if ($scope.action == "Eliminar") {
                            vm.enabled = true;
                        }
                    }
                }
                getDescripcionesStatus();
                getDescripcionesComisionPor();
                inicializar();
            }
        ]);
})();