/* global angular */
(function(angular) {
    'use strict';
    
    var appRequires = ['ngRoute', 'ngResource', 'ui.bootstrap', 'ngTable'];
    
    angular
        .module('tcc-app', appRequires)
        .config(config)
        .run(run);
        
    config.$inject = ['$routeProvider', '$httpProvider'];
        
    function config($routeProvider, $httpProvider) {
        $routeProvider
            .when('/', {
                templateUrl: 'app/views/home/index.html',
                controller: 'HomeController'
            })
            .when('/pesquisarLivros', {
                templateUrl: 'app/views/livro/pesquisar.html',
                controller: "PesquisarLivroController"
            })
            .when('/cadastrarLivro', {
                templateUrl: 'app/views/livro/cadastrar.html',
                controller: "CadastrarLivroController"
            });
            
        enableCors($httpProvider);
    };
    
    // Reference: http://www.codeproject.com/Articles/742532/Using-Web-API-Individual-User-Account-plus-CORS-En#enablecors
    function enableCors($httpProvider) {
        // Use x-www-form-urlencoded Content-Type
        $httpProvider.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';
        // Override $http service's default transformRequest
        $httpProvider.defaults.transformRequest = [function (data) {
            /**
             * The workhorse; converts an object to x-www-form-urlencoded serialization.
             * @param {Object} obj
             * @return {String}
             */
            var param = function (obj) {
                var query = '';
                var name, value, fullSubName, subName, subValue, innerObj, i;
                for (name in obj) {
                    value = obj[name];
                    if (value instanceof Array) {
                        for (i = 0; i < value.length; ++i) {
                            subValue = value[i];
                            fullSubName = name + '[' + i + ']';
                            innerObj = {};
                            innerObj[fullSubName] = subValue;
                            query += param(innerObj) + '&';
                        }
                    }
                    else if (value instanceof Object) {
                        for (subName in value) {
                            subValue = value[subName];
                            fullSubName = name + '[' + subName + ']';
                            innerObj = {};
                            innerObj[fullSubName] = subValue;
                            query += param(innerObj) + '&';
                        }
                    }
                    else if (value !== undefined && value !== null) {
                        query += encodeURIComponent(name) + '=' + encodeURIComponent(value) + '&';
                    }
                }
                return query.length ? query.substr(0, query.length - 1) : query;
            };
            return angular.isObject(data) && String(data) !== '[object File]' ? param(data) : data;
        }];
    };
    
    run.$inject = ['$templateCache'];
    
    function run($templateCache) {
        var homeIndexTpl = [
            '<div class="row">',
                '<div class="col-md-8">',
                    '<h3>TCC: Consumindo serviços Rest com AngularJS e Web API 2</h3>',
                '</div>',
            '</div>'
        ];
        
        $templateCache.put('app/views/home/index.html', homeIndexTpl.join(''));
        
        var pesquisarLivroTpl = [
            '<div class="row" ng-controller="PesquisarLivroController as vm">',
                '<div class="col-md-12 page-header" ng-init="vm.init()">',
                    '<form class="form-horizontal">',
                        '<fieldset>',
                            '<legend>Pesquisar livros por Usuário</legend>',
                            '<div class="form-group">',
                                '<label for="usuario" class="col-sm-2 control-label">Usuário:</label>',
                                '<div class="col-sm-10">',
                                    '<select id="usuario" class="form-control" ng-model="vm.pesquisaForm.usuario" ng-options="usuario.nome for usuario in vm.usuarios track by usuario.usuarioId">',
                                        '<option value="">Selecione um Usuário para pesquisa</option>',
                                    '</select>',
                                '</div>',
                            '</div>',   
                        '</fieldset>',
                        '<div class="text-right">',
                            '<button type="button" class="btn btn-success" ng-click="vm.pesquisar()" ng-disabled="vm.habilitarBtnPesquisar()">Pesquisar</button>',
                        '</div>',
                    '</form>',
                    '<hr>',
                    '<div class="table-responsive">',
                        '<table ng-table="vm.tableParams" class="table table-bordered table-hover">',
                            '<tr ng-repeat="livro in $data">',
                                '<td title="\'Titulo\'">',
                                    '{{livro.titulo}}',
                                '</td>',
                                '<td title="\'Editora\'">',
                                    '{{livro.editora}}',
                                '</td>',
                                '<td title="\'Ano Edição\'">',
                                    '{{livro.anoEdicao}}',
                                '</td>',
                            '</tr>',
                        '</table>',
                    '</div>',
                '</div>',
            '</div>'
        ];
        
        $templateCache.put('app/views/livro/pesquisar.html', pesquisarLivroTpl.join(''));
        
        var cadastrarLivroTpl = [
            '<div class="row" ng-controller="CadastrarLivroController as vm">',
                '<div class="col-md-12" ng-init="vm.init()">',
                    '<div class="row">',
                        '<div class="col-md-8">',
                            '<h3>Cadastrar livro</h3>',
                        '</div>',
                    '</div>',
                    '<hr/>',
                    '<form class="form-horizontal">',
                        '<div class="form-group">',
                            '<label class="col-sm-2 control-label">Usuário:</label>',
                            '<div class="col-sm-10">',
                                '<input type="text" class="form-control" id="usuarioNome" name="usuarioNome" disabled ng-model="vm.cadastroForm.usuarioNome" />',
                            '</div>',
                        '</div>',
                        '<div class="form-group">',
                            '<label for="titulo" class="col-sm-2 control-label"><span class="tcc-control-required">*</span> Título:</label>',
                            '<div class="col-sm-10">',
                                '<input type="text" class="form-control" id="titulo" name="titulo" placeholder="Título do livro" ng-model="vm.cadastroForm.titulo" />',
                            '</div>',
                        '</div>',
                        '<div class="form-group">',
                            '<label for="editora" class="col-sm-2 control-label">Editora:</label>',
                            '<div class="col-sm-10">',
                                '<input type="text" class="form-control" id="editora" name="editora" placeholder="Editora do livro" ng-model="vm.cadastroForm.editora" />',
                            '</div>',
                        '</div>',
                        '<div class="form-group">',
                            '<label for="edicao" class="col-sm-2 control-label">Edição:</label>',
                            '<div class="col-sm-4">',
                                '<input type="text" class="form-control" id="edicao" name="edicao" placeholder="Edição do livro" ng-model="vm.cadastroForm.edicao" />',
                            '</div>',
                            '<label for="anoEdicao" class="col-sm-2 control-label">Ano edição:</label>',
                            '<div class="col-sm-4">',
                                '<input type="text" class="form-control" id="anoEdicao" name="anoEdicao" placeholder="Ano edição do livro" ng-model="vm.cadastroForm.anoEdicao" />',
                            '</div>',
                        '</div>',
                        '<div class="form-group">',
                            '<label for="quantidadePaginas" class="col-sm-2 control-label">Qtde. Páginas:</label>',
                            '<div class="col-sm-4">',
                                '<input type="text" class="form-control" id="quantidadePaginas" name="quantidadePaginas" placeholder="Qtde páginas" ng-model="vm.cadastroForm.quantidadePaginas" />',
                            '</div>',
                            '<label for="quantidadeCapitulos" class="col-sm-2 control-label">Qtde capitulos:</label>',
                            '<div class="col-sm-4">',
                                '<input type="text" class="form-control" id="quantidadeCapitulos" name="quantidadeCapitulos" placeholder="Qtde capitulos" ng-model="vm.cadastroForm.quantidadeCapitulos" />',
                            '</div>',
                        '</div>',
                        '<div class="form-group">',
                            '<label for="categoriaId" class="col-sm-2 control-label">Categoria:</label>',
                            '<div class="col-sm-10">',
                                '<select id="categoriaId" class="form-control" ng-model="vm.cadastroForm.categoriaId" ng-options="categoria.categoriasDominioId as categoria.descricao for categoria in vm.categorias">',
                                    '<option value="">Selecione uma Categoria</option>',
                                '</select>',
                            '</div>',
                        '</div>',
                    '</form>',
                    '<div class="panel panel-default">',
                        '<div class="panel-heading">Autores do livro</div>',
                        '<div class="panel-body">',
                            '<div class="form-group col-sm-5">',
                                '<label for="categoriaId" class="control-label">Primeiro nome:</label>',
                                '<input type="text" class="form-control small-control-input" id="primeiroNome" name="primeiroNome" placeholder="Primeiro nome autor" ng-model="vm.autorForm.primeiroNome" />',
                            '</div>',
                            '<div class="form-group col-sm-5">',
                                '<label for="categoriaId" class="control-label">Ultimo nome:</label>',
                                '<input type="text" class="form-control small-control-input" id="ultimoNome" name="ultimoNome" placeholder="Ultimo nome autor" ng-model="vm.autorForm.ultimoNome" />',
                            '</div>',
                            '<div class="form-group col-sm-2 text-right">',
                                '<button type="button" class="btn btn-info" ng-click="vm.adicionarAutor()" ng-disabled="vm.habilitarBtnAdicionarAutor()">Adicionar Autor</button>',
                            '</div>',                            
                        '</div>',
                        '<div class="table-responsive">',
                            '<table class="table table-bordered table-hover">',
                                '<thead>',
                                    '<th>Primeiro nome</th>',
                                    '<th>Ultimo nome</th>',
                                '</thead>',
                                '<tbody>',
                                    '<tr class="tcc-empty-table" ng-if="vm.cadastroForm.autores.length === 0">',
                                        '<td colspan="2" class="text-center">Não há autores registrados para o livro</td>',
                                    '<tr>',
                                    '<tr ng-repeat="autor in vm.cadastroForm.autores">',
                                        '<td>{{autor.PrimeiroNome}}</td>',
                                        '<td>{{autor.UltimoNome}}</td>',
                                    '<tr>',
                                '</tbody>',
                            '</table>',
                        '</div>',
                    '</div>',
                    '<div class="text-right">',
                        '<button type="button" class="btn btn-success" ng-click="vm.salvar()" ng-disabled="vm.habilitarBtnSalvar()">Salvar</button>',
                    '</div>',
                '</div>',
            '</div>'
        ];
        
        $templateCache.put('app/views/livro/cadastrar.html', cadastrarLivroTpl.join(''));
    };
})(angular);