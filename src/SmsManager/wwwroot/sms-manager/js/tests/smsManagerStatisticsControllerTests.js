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
describe("Sms Manager Statistics Controller tests", function () {
    var SmsManagerService, http;
    var error500 = 'Server error';
    var vm;

    beforeEach(function () {
        module('smsManagerApp');
    });


    beforeEach(inject(function (smsManagerService, $httpBackend, $controller) {
        SmsManagerService = smsManagerService;
        http = $httpBackend;
        vm = $controller('smsManagerStatisticsController', { 'smsManagerService': SmsManagerService });
        http.whenGET(/.*/).respond(200, '');
    }));

    describe('On initial load', function () {

        it('variables should be defined.', function () {
            expect(vm.statistics).toEqual([]);
            expect(vm.dateFrom).not.toEqual('');
            expect(vm.dateTo).not.toEqual('');
            expect(vm.mccList).not.toEqual([]);
            expect(vm.error).toEqual(undefined);
        });

    });

    describe('On filter button pressed', function () {
        var responseMock = [{ "day": "2016-09-10", "mcc": "262", "pricePerSMS": 0.055, "count": 10, "totalPrice": 0.550 }, { "day": "2016-09-11", "mcc": "232", "pricePerSMS": 0.053, "count": 3, "totalPrice": 0.159 }, { "day": "2016-09-11", "mcc": "262", "pricePerSMS": 0.055, "count": 7, "totalPrice": 0.385 }];

        afterEach(function () {
            http.verifyNoOutstandingExpectation();
            http.verifyNoOutstandingRequest();
        });

        it('service should be called and on success populate result', function () {
            vm.filter();

            http.expect('GET', '../api/sms/statistics.json?dateFrom=' + vm.dateFrom + '&dateTo=' + vm.dateTo + '&mccList=262&mccList=232&mccList=260').respond(200, responseMock);
            http.flush();

            expect(vm.statistics).not.toEqual([]);
            expect(vm.statistics.length).not.toEqual(0);
            expect(vm.error).toEqual(undefined);
        });

        it('service should be called and on failure show error message', function () {
            vm.filter();

            http.expect('GET', '../api/sms/statistics.json?dateFrom=' + vm.dateFrom + '&dateTo=' + vm.dateTo + '&mccList=262&mccList=232&mccList=260').respond(500, error500);
            http.flush();

            expect(vm.statistics).toEqual([]);
            expect(vm.error).toEqual(error500);
        });
    });

});