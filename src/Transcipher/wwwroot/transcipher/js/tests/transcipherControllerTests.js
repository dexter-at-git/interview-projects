/// <reference path="../../../lib/jasmine/jasmine-core.js" />
/// <reference path="../../../lib/angular/angular.js" />
/// <reference path="../../../lib/angular-mocks/angular-mocks.js" />
/// <reference path="../../../lib/angular-busy/angular-busy.js" />
/// <reference path="../../../lib/angular-ui-router/angular-ui-router.js" />
/// <reference path="../transcipherapp.js" />
/// <reference path="../controllers/transciphercontroller.js" />
/// <reference path="../services/transcipherservice.js" />
describe("Transcipher Controller tests", function () {
    var TranscipherService, http;
    var algorithmsListMock = ["RSA", "Aes", "Morse"];
    var error500 = 'Server error';
    var vm;

    beforeEach(function () {
        module('transcipherApp');
    });

    afterEach(function () {
        http.verifyNoOutstandingExpectation();
        http.verifyNoOutstandingRequest();
    });

    beforeEach(inject(function (transcipherService, $httpBackend, $controller) {
        TranscipherService = transcipherService;
        http = $httpBackend;
        vm = $controller('transcipherController', { 'transcipherService': TranscipherService });
        http.whenGET(/.*/).respond(200, '');
    }));


    describe('On initial load', function () {

        it('variables should be defined and algorithm service should be called. If success populate algorithms variable.', function () {
            expect(vm.text).toEqual('');
            expect(vm.result).toEqual('');
            expect(vm.algorithms).toEqual([]);
            expect(vm.error).toEqual(undefined);
            expect(vm.selectedAlgorithm).toEqual(undefined);

            http.expect('GET', '../api/transcipher/algorithms').respond(200, algorithmsListMock);
            http.flush();

            expect(vm.algorithms.length).toEqual(algorithmsListMock.length);
            expect(vm.algorithms).toEqual(algorithmsListMock);
            expect(vm.error).toEqual(undefined);
        });

        it('variables should be defined and algorithm service should be called. If failure show error.', function () {
            expect(vm.text).toEqual('');
            expect(vm.result).toEqual('');
            expect(vm.algorithms).toEqual([]);
            expect(vm.error).toEqual(undefined);
            expect(vm.selectedAlgorithm).toEqual(undefined);

            http.expect('GET', '../api/transcipher/algorithms').respond(500, error500);
            http.flush();

            expect(vm.algorithms).toEqual([]);
            expect(vm.error).toEqual(error500);
        });
    });


    describe('On encrypt button pressed', function () {
        var encryptResult = 'encrypt result';
        var encryptError = 'error';

        beforeEach(function () {
            http.expect('GET', '../api/transcipher/algorithms').respond(200, algorithmsListMock);
            http.flush();
        });

        it('service should be called and on success populate result', function () {
            vm.selectedAlgorithm = algorithmsListMock[1];
            vm.text = 'test message';

            var processingData = {
                algorithm: algorithmsListMock[1],
                text: 'test message'
            }

            vm.encrypt();

            http.expect('POST', '../api/transcipher/encrypt', processingData).respond(200, encryptResult);
            http.flush();

            expect(vm.result).not.toEqual('');
            expect(vm.result).toEqual(encryptResult);
            expect(vm.error).toEqual(undefined);
        });

        it('service should be called and on failure show error message', function () {
            vm.selectedAlgorithm = algorithmsListMock[1];
            vm.text = 'test message';

            var processingData = {
                algorithm: algorithmsListMock[1],
                text: 'test message'
            }

            vm.encrypt();

            http.expect('POST', '../api/transcipher/encrypt', processingData).respond(500, encryptError);
            http.flush();

            expect(vm.result).toEqual('');
            expect(vm.error).toEqual(encryptError);
        });
    });


    describe('On decrypt button pressed', function () {
        var decryptResult = 'decrypt result';
        var decryptError = 'error';

        beforeEach(function () {
            http.expect('GET', '../api/transcipher/algorithms').respond(200, algorithmsListMock);
            http.flush();

            vm.selectedAlgorithm = algorithmsListMock[1];
            vm.text = 'test message';
        });

        it('service should be called and on success populate result', function () {
            var processingData = {
                algorithm: algorithmsListMock[1],
                text: 'test message'
            }

            vm.decrypt();

            http.expect('POST', '../api/transcipher/decrypt', processingData).respond(200, decryptResult);
            http.flush();

            expect(vm.result).not.toEqual('');
            expect(vm.result).toEqual(decryptResult);
            expect(vm.error).toEqual(undefined);
        });

        it('service should be called and on failure show error message', function () {
            var processingData = {
                algorithm: algorithmsListMock[1],
                text: 'test message'
            }

            vm.decrypt();

            http.expect('POST', '../api/transcipher/decrypt', processingData).respond(500, decryptError);
            http.flush();

            expect(vm.result).toEqual('');
            expect(vm.error).toEqual(decryptError);
        });
    });


    describe('On selected algorithm change', function () {

        beforeEach(function () {
            http.expect('GET', '../api/transcipher/algorithms').respond(200, algorithmsListMock);
            http.flush();
        });

        it('result and error should be reseted', function () {
            vm.error = 'error';
            vm.text = 'test message';

            vm.algorithmChange();

            expect(vm.result).toEqual('');
            expect(vm.error).toEqual(undefined);
        });
    });
});