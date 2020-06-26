/* global jQuery */
/* global angular */
(function (app) {
    'use strict';

    //Declaração de um novo controller
    app.controller('CadastrarLivroController', cadastrarLivroController);
    
    // Injeção de dependência da "apiService"
    cadastrarLivroController.$inject = ['apiService'];

    //Registrando o controller e suas funções
    function cadastrarLivroController(apiService) { 
        // $scope = vm
        var vm = this;
        
        // Executado ao inicializar a renderização da tela
        vm.init = function() {
            vm.cadastroForm = {
                usuarioId: '5705db9e-a230-4d5b-9ef6-8ecda44e56a9',
                usuarioNome: 'Ricardo Concordia',
                autores: []
            };
            
            vm.autorForm = {};
            
            vm.obterCategorias();
        };
        
        // Chamada aos serviços

        vm.obterCategorias = function() {
            apiService.get('Categorias/ObterCategorias', null, vm.atualizarCategorias);
        };

        vm.salvar = function() {
            if(vm.validarFormularioCadastro()) {
                apiService.post('Livros/CadastrarLivro', vm.cadastroForm, vm.verificarSucessoAoSalvar, vm.verificarFalhaAoSalvar);
            }
        };

        // Validações do formulário


        vm.habilitarBtnSalvar = function() {
            return !vm.formularioCadastroEstaValido();
        };
        
        vm.validarFormularioCadastro = function() {
            var estaValido = vm.formularioCadastroEstaValido(); 
            
            if(!estaValido) {
                vm.exibirMensagem('Formulário de cadastro está inválido');
            }
            
            return estaValido;
        };

        vm.formularioCadastroEstaValido = function() {
            return !vm.valorEstaVazioOuNulo(vm.cadastroForm.titulo);
        };

        vm.validarFormularioAutor = function() {
            var estaValido = vm.formularioAutorEstaValido(); 
            
            if(!estaValido) {
                vm.exibirMensagem('Formulário de autor está inválido');
            }
            
            return estaValido;
        };
        
        vm.formularioAutorEstaValido = function() {
            return !vm.valorEstaVazioOuNulo(vm.autorForm.primeiroNome) && !vm.valorEstaVazioOuNulo(vm.autorForm.ultimoNome);
        };

        vm.valorEstaVazioOuNulo = function(valor) {
            return valor === undefined || valor === '' || jQuery.trim(valor) === '';
        }

        // Atualizando dados do scope
        
        vm.atualizarCategorias = function(result) {
            vm.categorias = result.data;
        };
        
        
        vm.adicionarAutor = function() {
            if(vm.validarFormularioAutor()) {
                vm.cadastroForm.autores.push({
                    PrimeiroNome: vm.autorForm.primeiroNome,
                    UltimoNome: vm.autorForm.ultimoNome
                });
                
                vm.limparFormularioAutor();
            }
        };
        
        // Ações de notificação
        
        vm.verificarSucessoAoSalvar = function(result) {
            console.log('Sucesso ao cadastrar: ', result);
            vm.limparFormularioAutor();
            vm.limparFormularioCadastro();
        };
        
        vm.verificarFalhaAoSalvar = function(error) {
            console.log('Falha ao cadastrar: ', error);
        };
        
        vm.habilitarBtnAdicionarAutor = function() {            
            return !vm.formularioAutorEstaValido();
        };
        
        vm.exibirMensagem = function(msg) {
            alert(msg);
        };
        
        // Limpar formulário
        
        vm.limparFormularioAutor = function() {
            vm.autorForm.primeiroNome = null;
            vm.autorForm.ultimoNome = null;
        };

        vm.limparFormularioCadastro = function() {
            vm.cadastroForm.titulo = null;
            vm.cadastroForm.editora = null;
            vm.cadastroForm.edicao = null;
            vm.cadastroForm.anoEdicao = null;
            vm.cadastroForm.quantidadePaginas = null;
            vm.cadastroForm.quantidadeCapitulos = null;
            vm.cadastroForm.categoriaId = "";
            vm.cadastroForm.autores = [];
        }
    };
})(angular.module('tcc-app'));