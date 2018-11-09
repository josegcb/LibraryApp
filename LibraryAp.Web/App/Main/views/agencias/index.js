(function () {
    angular.module('app').controller('app.views.agencias.index', [
        "$http", '$scope', '$timeout', '$uibModal', 'abp.services.app.agencia', '$rootScope',
        function ($http, $scope, $timeout, $uibModal, vService, $rootScope) {
            var vm = this;
            vm.records = [];
            vm.PropertiesForSearch = [];
            vm.Filters = [];
            vm.CounterFilters = 0;
           

            vm.openModalForChoose = function () {
                var modalInstance = $uibModal.open({
                    templateUrl: '/App/Main/views/agencias/escoger.cshtml',
                    controller: 'app.views.agencias.escoger as vm',
                    backdrop: 'static'
                });
                modalInstance.result.then(function () {
                    Search();
                });
            };

            vm.openModalForNew = function () {
                openModalForAction(0, "Insertar");
            };
                
            vm.openModalForEdit = function (data) {
                openModalForAction(data.id, "Modificar");                
            };

            vm.openModalForDelete = function (data) {
                openModalForAction(data.id, "Eliminar");
            }

            vm.openModalForRead = function (agencia) {
                abp.message.confirm("Consultar");
                openModalForAction(data.id, "Consultar");
            }

            vm.refresh = function () {
                Search();
            };

            function OpenModalForAction(idRecord, action) {
                var modalInstance = $uibModal.open({
                    templateUrl: '/App/Main/views/agencias/input.cshtml',
                    controller: 'app.views.agencias.input as vm',
                    backdrop: 'static',
                    resolve: {
                        parameters: {
                            idRecord: 0,
                            action: "Insertar"
                        }
                    }
                });
                modalInstance.rendered.then(function () {
                    $.AdminBSB.input.activate();
                });
                modalInstance.result.then(function () {
                    Search();
                });
            }

            function Search() {
                $http({
                    method: "POST",
                    url: "/Agencia/ListAll",
                    dataType: 'json',
                    headers: {
                        "Content-Type": "application/json"
                    }
                }).then(function (result) {
                    vm.records = result.data;
                });
            }

            function SearchByFilter() {
                var JsonList = JSON.stringify(vm.filters);
                $http({
                    method: "POST",
                    url: "/Agencia/ListAllByFilters",
                    dataType: 'json',
                    data: {
                        vData: JsonList
                    },
                    headers: {
                        "Content-Type": "application/json"
                    }
                }).then(function (result) {
                    vm.records = result.data;
                });
            }

            function PropertiesForFilters() {
                $http({
                    method: "POST",
                    url: "/Agencia/ListProperties",
                    dataType: 'json',
                    headers: {
                        "Content-Type": "application/json"
                    }
                }).then(function (result) {
                    vm.propertiesforsearch = result.data;
                });
            }

            function AddFilterDiv() {
                var NewFilterDiv = $(document.createElement('div'))
                    .attr("id", 'FilterDiv' + counter)
                    .attr("class", 'form-inline');
                NewFilterDiv.after().html(
                    '<div class="form-inline">'
                    + '<label for="cmbPropiedad' + counter + '">Campo:</label>'
                    + '<select  class="form-control"  name="cmbPropiedad' + counter + '" id="cmbPropiedad' + counter + '"></select>'
                    + '<label for="cmbOperador' + counter + '">Operador:</label>'
                    + '<select class="form-control" name="cmbOperador' + counter + '" id="cmbOperador' + counter + '"></select>'
                    + '<label>Valor:</label>'
                    + '<input class="form-control "type="text" name="txtValue1' + counter + '" id="txtValue1' + counter + '" value="" >'
                    + '<input class="form-control "type="text" name="txtValue2' + counter + '" id="txtValue2' + counter + '" value="" >'
                    + '<select class="form-control" name="cmbValue' + counter + '" id="cmbValue' + counter + '"></select>'
                    + '<select class="form-control" name="cmbConector' + counter + '" id="cmbConector' + counter + '">'
                    + '<option value="0">Y</option>'
                    + '<option value="1">O</option>'
                    + '</select > '
                    + '</div>');
                NewFilterDiv.appendTo("#FiltersGroup");
                let dropdownPropiedad = $('#cmbPropiedad' + counter);
                dropdownPropiedad.empty();
                dropdownPropiedad.append('<option selected="true" disabled>Seleccione un Campo</option>');
                $.each(vm.propertiesforsearch, function (key, entry) {
                    dropdownPropiedad.append($('<option></option>').attr('value', entry.tipo).text(entry.nombre));
                });
                dropdownPropiedad.prop('selectedIndex', 0);
                let dropdownOperador = $('#cmbOperador' + vm.CounterFilters);
                dropdownOperador.empty();
                dropdownOperador.append('<option selected="true" disabled>Seleccione un Operador</option>');
                dropdownOperador.prop('selectedIndex', 0);
                let dropdownValue = $('#cmbValue' + vm.CounterFilters);
                dropdownValue.empty();
                dropdownValue.append('<option selected="true" disabled>Seleccione un Valor</option>');
                dropdownValue.prop('selectedIndex', 0);
                txtValue1.style.display = 'none';
                txtValue2.style.display = 'none';
                dropdownValue.hide();
            }

            function AddtxtValue1Event() {
                var txtValue1 = document.getElementById('txtValue1' + vm.CounterFilters);
                txtValue1.addEventListener("keydown", function (e) {
                    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                        (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                        (e.keyCode == 67 && (e.ctrlKey === true || e.metaKey === true)) ||
                        (e.keyCode == 88 && (e.ctrlKey === true || e.metaKey === true)) ||
                        (e.keyCode >= 35 && e.keyCode <= 39)) {
                        return;
                    }
                    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                        e.preventDefault();
                    }
                });
            }

            function AddtxtValue1Event() {
                var txtValue2 = document.getElementById('txtValue2' + vm.CounterFilters);
                txtValue2.addEventListener("keydown", function (e) {
                    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                        (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                        (e.keyCode == 67 && (e.ctrlKey === true || e.metaKey === true)) ||
                        (e.keyCode == 88 && (e.ctrlKey === true || e.metaKey === true)) ||
                        (e.keyCode >= 35 && e.keyCode <= 39)) {
                        return;
                    }
                    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                        e.preventDefault();
                    }
                });
            }

            function AdddropdownPropiedadEvents() {
                var dropdownPropiedad = document.getElementById('cmbPropiedad' + vm.CounterFilters);
                let dropdownOperador = $('#cmbOperador' + vm.CounterFilters);
                let dropdownValue = $('#cmbValue' + vm.CounterFilters);
                dropdownPropiedad.addEventListener("change", function () {
                    dropdownOperador.empty();
                    dropdownOperador.append('<option selected="true" disabled>Seleccione un Operador</option>');
                    dropdownValue.empty();
                    dropdownValue.append('<option selected="true" disabled>Seleccione un Valor</option>');

                    $.each(vm.propertiesforsearch, function (key, entry) {
                        if (entry.nombre == dropdownPropiedad.options[dropdownPropiedad.selectedIndex].text) {
                            $.each(entry.operaciones, function (keyOp, entryOp) {
                                dropdownOperador.append($('<option></option>').attr('value', entryOp.value).text(entryOp.descripcion));
                            });
                            if (entry.tipo == "Decimal") {
                                AddtxtValue1Event();
                                AddtxtValue2Event();
                              
                            }
                            if (entry.tipo == "Enum") {
                                $.each(entry.valores, function (keyOp, entryOp) {
                                    dropdownValue.append($('<option></option>').attr('value', entryOp.value).text(entryOp.descripcion));
                                });
                            }
                        }
                    });
                    var txtValue1 = document.getElementById('txtValue1' + vm.CounterFilters);
                    var txtValue2 = document.getElementById('txtValue2' + vm.CounterFilters);
                    dropdownOperador.prop('selectedIndex', 0);
                    txtValue1.style.display = 'none';
                    txtValue2.style.display = 'none';
                    txtValue1.value = "";
                    txtValue2.value = "";
                    dropdownValue.hide();
                    dropdownValue.prop('selectedIndex', 0);
                });
            }

            function InicilizeControlsValue(valnumeroDeParametros, valtipo) {
                let dropdownValue = $('#cmbValue' + vm.CounterFilters);
                var txtValue1 = document.getElementById('txtValue1' + vm.CounterFilters);
                var txtValue2 = document.getElementById('txtValue2' + vm.CounterFilters);
                txtValue1.value = "";
                txtValue2.value = "";
                if (numeroDeParametros == 0) {
                    txtValue1.style.display = 'none';
                    txtValue2.style.display = 'none';
                    dropdownValue.hide();
                } else if (numeroDeParametros == 1) {
                    if (tipo == "Enum") {
                        txtValue1.style.display = 'none';
                        txtValue2.style.display = 'none';
                        dropdownValue.show();
                    } else {
                        txtValue1.style.display = '';
                        txtValue2.style.display = 'none';
                        dropdownValue.hide();
                    }
                } else {
                    txtValue1.style.display = '';
                    txtValue2.style.display = '';
                    dropdownValue.hide();
                }

            }

            function AdddropdownOperadorEvents() {               
                var cmbOperador = document.getElementById('cmbOperador' + vm.CounterFilters);
                cmbOperador.addEventListener("change", function () {
                    var numeroDeParametros = 0;
                    var tipo = "";
                    $.each(vm.propertiesforsearch, function (key, entry) {
                        if (entry.nombre == dropdownPropiedad.options[dropdownPropiedad.selectedIndex].text) {
                            $.each(entry.operaciones, function (keyOp, entryOp) {
                                if (entryOp.value == cmbOperador.value) {
                                    numeroDeParametros = entryOp.numeroDeParametros;
                                }
                            });
                            tipo = entry.tipo;
                        }
                    });
                    InicilizeControlsValue(numeroDeParametros, tipo )
                });
            }

            function AddFilterEvents() {
                AdddropdownPropiedadEvents();
                AdddropdownOperadorEvents
                
            }


            Search();
            PropertiesForFilters();


            vm.addFilter = function () {
                if (vm.CounterFilters > 10) {
                    alert("Solo se permiten 10 Filtros");
                    return false;
                }
                if (vm.PropertiesForSearch.length == 0) {
                    alert("No Existen filtros para este modulo");
                    return false;
                }
                AddFilterDiv();
                AddFilterEvents()
                vm.CounterFilters++;
            }


            $(document).ready(function () {
                vm.counterfilters = 1;
              
                   

                $("#removeFilter").click(function () {
                    if (counter == 0) {
                        alert("No Existen filtros");
                        return false;
                    }
                    counter--;
                    $("#FilterDiv" + counter).remove();
                });

                $("#executeFilter").click(function () {
                    var list = [];
                    for (i = 1; i < counter; i++) {
                        var dropdownPropiedad = document.getElementById('cmbPropiedad' + i);
                        var valueToPush = {};
                        valueToPush.TypeName = $('#cmbPropiedad' + i).val();
                        if (valueToPush.TypeName != null) {
                            valueToPush.PropertyName = dropdownPropiedad.options[dropdownPropiedad.selectedIndex].text;
                            valueToPush.BooleanOperator = $('#cmbOperador' + i).val();
                            valueToPush.LogicOperator = $('#cmbConector' + i).val();
                            valueToPush.Value1 = $('#txtValue1' + i).val();
                            valueToPush.Value2 = $('#txtValue2' + i).val();
                            valueToPush.Value3 = $('#cmbValue' + i).val();
                            list.push(valueToPush);
                        }
                    }
                    vm.filters = list;                    
                    SearchByFilter();                    
                });
            });
        }
    ]);
})();