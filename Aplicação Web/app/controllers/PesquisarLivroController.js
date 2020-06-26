(function (app) {
    'use strict';

    //Declarando um controller de pesquisa e injetando suas dependências
    app.controller('PesquisarLivroController', pesquisarLivroController);
    
    pesquisarLivroController.$inject = ['NgTableParams', 'apiService'];

    //Registrando o controller e suas funções
    function pesquisarLivroController(NgTableParams, apiService) { 
        var vm = this;
        
        vm.pesquisaForm = {};
        
        // Executado ao inicializar a renderização da tela
        vm.init = function() {
            //Instância da dependência NgTableParams para realizar paginação de registros e requisições ao back-end da API
            vm.tableParams = new NgTableParams({
                page: 1,
                count: 2
            }, {
                counts: [],
                getData: function($defer, ngTableParams) {
                    var url = vm.criarUrlComParametros('Livros', 'ObterTodosLivros', {
                        usuarioId: '5705DB9E-A230-4D5B-9EF6-8ECDA44E56A9',
                        pagina: ngTableParams.page()
                    });
                    
                    apiService.get(url, null, function(result) {
                        ngTableParams.total(result.data.total);
                        $defer.resolve(result.data.dto);
                    });
                }
            });
        };
        
        // Formata a URL de consulta de acordo com o formato padrão de uma Query String

        vm.criarUrlComParametros = function(nomeController, nomeAcao, parametros) {
            var urlBase = nomeController +'/'+ nomeAcao;
        
            if (parametros) {
                urlBase = vm.adicionarParametrosNaUrl(urlBase, parametros);
            }
        
            return urlBase;
        };
        
        vm.adicionarParametrosNaUrl = function(urlBase, parametros) {
            var parametrosFormatados = [];
        
            if (parametros) {
                for (var parametro in parametros) {
                    parametrosFormatados.push(parametro + '=' + encodeURIComponent(parametros[parametro]));
                }
        
                urlBase += '?' + parametrosFormatados.join('&');
            }
        
            return urlBase;
        };
        
        // Usuário fixo no sistema
        vm.usuarios = [{
            usuarioId: '5705db9e-a230-4d5b-9ef6-8ecda44e56a9',
            nome: 'Ricardo Concordia'
        }];
        
        // Ações de notificação
        vm.pesquisar = function() {
            console.log('Pesquisar: ', vm.pesquisaForm.usuario);
        };
        
        // Liberação do botão de pesquisa de acordo com condições de seleção do usuário
        vm.habilitarBtnPesquisar = function() {
            return !angular.isDefined(vm.pesquisaForm.usuario) || vm.pesquisaForm.usuario === null;
        };
    };
})(angular.module('tcc-app'));