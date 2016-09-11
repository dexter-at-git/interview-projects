/// <reference path="../../../lib/jasmine/jasmine-core.js" />
/// <reference path="../../../lib/angular/angular.js" />
/// <reference path="../../../lib/angular-mocks/angular-mocks.js" />
/// <reference path="../../../lib/angular-busy/angular-busy.js" />
/// <reference path="../../../lib/angular-ui-router/angular-ui-router.js" />
/// <reference path="../smsmanagerapp.js" />
/// <reference path="../controllers/smsmanagersendcontroller.js" />
/// <reference path="../controllers/smsmanagerbrowsecontroller.js" />
/// <reference path="../controllers/smsmanagerstatisticscontroller.js" />
/// <reference path="../services/smsmanagerservice.js" />
describe("Sms Manager Send Controller tests", function () {
    var SmsManagerService, http;
    var error500 = 'Server error';
    var vm;

    beforeEach(function () {
        module('smsManagerApp');
    });

    beforeEach(inject(function (smsManagerService, $httpBackend, $controller) {
        SmsManagerService = smsManagerService;
        http = $httpBackend;
        vm = $controller('smsManagerSendController', { 'smsManagerService': SmsManagerService });
        http.whenGET(/.*/).respond(200, '');
    }));

    describe('On initial load', function () {

        it('variables should be defined.', function () {
            expect(vm.message).not.toEqual('');
            expect(vm.to).not.toEqual('');
            expect(vm.from).not.toEqual('');
            expect(vm.error).toEqual(undefined);
            expect(vm.result).toEqual('');
        });

    });


    describe('On send button pressed', function () {

        afterEach(function () {
            http.verifyNoOutstandingExpectation();
            http.verifyNoOutstandingRequest();
        });

        beforeEach(function () {
            vm.to = 'to';
            vm.from = 'from';
            vm.message = 'message';
        });

        it('service should be called and on success populate result', function () {
            vm.send();

            http.expect('GET', '../api/sms/send.json?to=' + vm.to + '&from=' + vm.from + '&text=' + vm.message).respond(200);
            http.flush();

            expect(vm.result).not.toEqual('');
            expect(vm.error).toEqual(undefined);
        });

        it('service should be called and on failure show error message', function () {
            vm.send();

            http.expect('GET', '../api/sms/send.json?to=' + vm.to + '&from=' + vm.from + '&text=' + vm.message).respond(500, error500);
            http.flush();

            expect(vm.result).toEqual('');
            expect(vm.error).toEqual(error500);
        });
    });

});