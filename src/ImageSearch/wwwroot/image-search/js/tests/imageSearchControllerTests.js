/// <reference path="../../../lib/jasmine/jasmine-core.js" />
/// <reference path="../../../lib/angular/angular.js" />
/// <reference path="../../../lib/angular-mocks/angular-mocks.js" />
/// <reference path="../../../lib/angular-busy/angular-busy.js" />
/// <reference path="../../../lib/angular-ui-router/angular-ui-router.js" />
/// <reference path="../imagesearchapp.js" />
/// <reference path="../controllers/imagesearchcontroller.js" />
/// <reference path="../services/modeltransformer.js" />
/// <reference path="../services/localstorageservice.js" />
/// <reference path="../services/imagesearchservice.js" />
describe("Image serach tests", function () {
    var ImageSearchService, LocalStorageService, http;
    var searchHistoryMock = [{ "key": "1471539932146", "date": "2016-08-18T17:05:32.146Z", "searchResults": [{ "url": "http://cdn.gadgets360.com/content/assets/brands/apple.png?quality=80&output-format=jpg", "title": "apple.png", "thumbnailUrl": "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcQ9jq-f7Q5jUbj3ESHUWzyVAIMjFz3p8WUov3_11w44d5Nd_ayjU43Z-w" }, { "url": "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fa/Apple_logo_black.svg/200px-Apple_logo_black.svg.png", "title": "200px-Apple_logo_black.svg.png", "thumbnailUrl": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT0QxDbV8shJz7zvxTeSX3Rf1TWMwM7noKhdggeEHyOUiLy0QpQz8Y0_w" }, { "url": "https://support.apple.com/library/content/dam/edam/applecare/images/en_US/applecare/region-us-ca-pr-nav.png", "title": "region-us-ca-pr-nav.png", "thumbnailUrl": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRlepJlOMtGGUTdzRkS4v5c2QfEzzSLQQiDJKp06wseBd0-PeDzs9ldog" }, { "url": "https://www.apple.com/ac/structured-data/images/knowledge_graph_logo.png?201604140847", "title": "knowledge_graph_logo.png", "thumbnailUrl": "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcTYn78Ksh9Ap3JMc7EiR1DFoe3tsYlSGlrrqG5gxPRFXshjRIXLNX4e1g" }, { "url": "http://img.etimg.com/thumb/msid-50660935,width-310,resizemode-4,imglength-8035/apple-posts-record-india-sales-in-tough-october-december-quarter.jpg", "title": "apple-posts-record-india-sales-in-tough-october-december-quarter.jpg", "thumbnailUrl": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTJPj_-gSC3qur68Rz_cx_U05eBVXTpIAh_4aQ8wuyQmbQtQldNflp3Wg" }, { "url": "http://metropolitan.fi/files/2016-07/apple-logo-black.svg.png", "title": "apple-logo-black.svg.png", "thumbnailUrl": "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcTYrLl3OUFL_vwnlVSyv4OQcgIVM7sK0tt04XMSZ09TbzGmTC6NQijg_M0" }, { "url": "http://www.chatswoodchasesydney.com.au/media/13792/apple-logo.jpg.ashx?width=300&height=300&preset=default", "title": "apple-logo.jpg.ashx", "thumbnailUrl": "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcSH6ow51rdLOZlVeR-zqpjsG5vnO0aiZjhjEHfwSC4sQ-8w61lTRaA7TA" }] }, { "key": "1471539941939", "date": "2016-08-18T17:05:41.939Z", "searchResults": [{ "url": "https://files.allaboutbirds.net/wp-content/themes/html5blank-stable/images/blue-winged-warbler.jpg", "title": "blue-winged-warbler.jpg", "thumbnailUrl": "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcTtOZcQmiKjU9meN4NctRKt-6lQsRu-_nKLobjN6_Dn0ViscN7pPyxfci0" }, { "url": "http://animalia-life.com/data_images/bird/bird4.jpg", "title": "bird4.jpg", "thumbnailUrl": "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcRdgaJFf4UTtjdZ2B_UFhY7hOoE6OR2m5DHfpS_5UX25pw9Cf_Esrs6V24" }, { "url": "https://s-media-cache-ak0.pinimg.com/236x/e3/69/7b/e3697b52bbcdf277a3712b83e816cc43.jpg", "title": "e3697b52bbcdf277a3712b83e816cc43.jpg", "thumbnailUrl": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRYqccYKaQBowSME_3ab6GAub5V2l6mlRWqJ1CcdHcD0XN4Z7Jz955u4w" }, { "url": "http://newswatch.nationalgeographic.com/files/2014/08/European-Bee-eater-Lennart-Hessel-Bird-Watch-photo.jpg", "title": "European-Bee-eater-Lennart-Hessel-Bird-Watch-photo.jpg", "thumbnailUrl": "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcSy5ZpyW4e_K9E2hjFE1_jPgo1NTFSOeC_WqlQ7v9P65tZXTPgDkA4xdQ" }, { "url": "https://s-media-cache-ak0.pinimg.com/236x/0a/74/3e/0a743ea8bf37ee55cf5b15620c00a334.jpg", "title": "0a743ea8bf37ee55cf5b15620c00a334.jpg", "thumbnailUrl": "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcSwFf2ffwap69ltr9PgLJ4U9jN2d6_F1XihuCxcEpi_u0tke8DSql2XkuE" }, { "url": "https://abcbirds.org/wp-content/uploads/2015/04/birds_botw_navthumb_rufous-hummingbird_tmore-campbell_ss.png", "title": "birds_botw_navthumb_rufous-hummingbird_tmore-campbell_ss.png", "thumbnailUrl": "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcTh320uxK2AWaNX-OcSr6feYPLG4IYH8kftdRjBxqv163XUB9drLwC3tw" }, { "url": "http://static.newworldencyclopedia.org/thumb/2/27/Bird.parts.jpg/300px-Bird.parts.jpg", "title": "300px-Bird.parts.jpg", "thumbnailUrl": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS8gUsx1fspVE5pWAKt620EgJV0qGCpIw6JzmfPGvO_nynPpucZ11wGKA" }, { "url": "http://www.birds.cornell.edu/bbimages/clo/images/wedo/rss_ml_tropicalbirdrecording.jpg", "title": "rss_ml_tropicalbirdrecording.jpg", "thumbnailUrl": "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcSztoRlKmZLZLOLvzu9tjp3WeEDyWWlClsOJLVMcHrfl65LEJIKNg3IqFs" }, { "url": "https://upload.wikimedia.org/wikipedia/commons/thumb/3/34/Callipepla_californica2_(edit1).jpg/270px-Callipepla_californica2_(edit1).jpg", "title": "270px-Callipepla_californica2_(edit1).jpg", "thumbnailUrl": "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcRZckV2vxwNgUB17XMmaNsGV_wY23Ts6gHTmYrUDWvbtYexJhL4ZhaZ0j0" }, { "url": "http://thegabrielfoundation.org/wp-content/uploads/2015/11/Chandi-9-11-15-3-e1448649911607.jpg", "title": "Chandi-9-11-15-3-e1448649911607.jpg", "thumbnailUrl": "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcQu29hdf4SZd7sRbkEKamAXRJ4Vk08kIPC1jUsR9zqjrbtIhhtZ8UMmZg" }] }, { "key": "1471539946746", "date": "2016-08-18T17:05:46.746Z", "searchResults": [{ "url": "https://files.allaboutbirds.net/wp-content/themes/html5blank-stable/images/blue-winged-warbler.jpg", "title": "blue-winged-warbler.jpg", "thumbnailUrl": "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcTtOZcQmiKjU9meN4NctRKt-6lQsRu-_nKLobjN6_Dn0ViscN7pPyxfci0" }, { "url": "https://s-media-cache-ak0.pinimg.com/236x/e3/69/7b/e3697b52bbcdf277a3712b83e816cc43.jpg", "title": "e3697b52bbcdf277a3712b83e816cc43.jpg", "thumbnailUrl": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRYqccYKaQBowSME_3ab6GAub5V2l6mlRWqJ1CcdHcD0XN4Z7Jz955u4w" }, { "url": "https://s-media-cache-ak0.pinimg.com/236x/0a/74/3e/0a743ea8bf37ee55cf5b15620c00a334.jpg", "title": "0a743ea8bf37ee55cf5b15620c00a334.jpg", "thumbnailUrl": "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcSwFf2ffwap69ltr9PgLJ4U9jN2d6_F1XihuCxcEpi_u0tke8DSql2XkuE" }, { "url": "https://abcbirds.org/wp-content/uploads/2015/04/birds_botw_navthumb_rufous-hummingbird_tmore-campbell_ss.png", "title": "birds_botw_navthumb_rufous-hummingbird_tmore-campbell_ss.png", "thumbnailUrl": "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcTh320uxK2AWaNX-OcSr6feYPLG4IYH8kftdRjBxqv163XUB9drLwC3tw" }, { "url": "http://www.birds.cornell.edu/bbimages/clo/images/wedo/rss_ml_tropicalbirdrecording.jpg", "title": "rss_ml_tropicalbirdrecording.jpg", "thumbnailUrl": "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcSztoRlKmZLZLOLvzu9tjp3WeEDyWWlClsOJLVMcHrfl65LEJIKNg3IqFs" }, { "url": "https://upload.wikimedia.org/wikipedia/commons/thumb/3/34/Callipepla_californica2_(edit1).jpg/270px-Callipepla_californica2_(edit1).jpg", "title": "270px-Callipepla_californica2_(edit1).jpg", "thumbnailUrl": "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcRZckV2vxwNgUB17XMmaNsGV_wY23Ts6gHTmYrUDWvbtYexJhL4ZhaZ0j0" }, { "url": "http://thegabrielfoundation.org/wp-content/uploads/2015/11/Chandi-9-11-15-3-e1448649911607.jpg", "title": "Chandi-9-11-15-3-e1448649911607.jpg", "thumbnailUrl": "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcQu29hdf4SZd7sRbkEKamAXRJ4Vk08kIPC1jUsR9zqjrbtIhhtZ8UMmZg" }] }];
    var searchResultsMock = [{ "url": "https://tctechcrunch2011.files.wordpress.com/2014/06/apple_topic.png?w=220", "title": "apple_topic.png", "thumbnailUrl": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQucpFQmcJFGHVxlrljW1SOBKzbcvRBtD3qNPW4tTmXKBxGlYq6LuhfWDg" }, { "url": "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fa/Apple_logo_black.svg/150px-Apple_logo_black.svg.png", "title": "150px-Apple_logo_black.svg.png", "thumbnailUrl": "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcTA3dcuLTwWxd2ldneGsm_0R5P1rKGBnl8XhvX8Ku01nT6YM2FtLOH0JA" }, { "url": "http://cdn.gadgets360.com/content/assets/brands/apple.png?quality=80&output-format=jpg", "title": "apple.png", "thumbnailUrl": "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcQ9jq-f7Q5jUbj3ESHUWzyVAIMjFz3p8WUov3_11w44d5Nd_ayjU43Z-w" }, { "url": "https://support.apple.com/content/dam/edam/applecare/images/en_US/promo_icons/large-promo-icon-twitter-blue_2x.png", "title": "large-promo-icon-twitter-blue_2x.png", "thumbnailUrl": "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcT2AbxaOfGKaerALwhRxZ2cYkNsFxoZn3N3yek5QHOQJ2rlo8pSgmNk_A" }, { "url": "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fa/Apple_logo_black.svg/200px-Apple_logo_black.svg.png", "title": "200px-Apple_logo_black.svg.png", "thumbnailUrl": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT0QxDbV8shJz7zvxTeSX3Rf1TWMwM7noKhdggeEHyOUiLy0QpQz8Y0_w" }, { "url": "https://support.apple.com/library/content/dam/edam/applecare/images/en_US/applecare/region-us-ca-pr-nav.png", "title": "region-us-ca-pr-nav.png", "thumbnailUrl": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRlepJlOMtGGUTdzRkS4v5c2QfEzzSLQQiDJKp06wseBd0-PeDzs9ldog" }, { "url": "https://www.apple.com/ac/structured-data/images/knowledge_graph_logo.png?201604140847", "title": "knowledge_graph_logo.png", "thumbnailUrl": "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcTYn78Ksh9Ap3JMc7EiR1DFoe3tsYlSGlrrqG5gxPRFXshjRIXLNX4e1g" }, { "url": "http://img.etimg.com/thumb/msid-50660935,width-310,resizemode-4,imglength-8035/apple-posts-record-india-sales-in-tough-october-december-quarter.jpg", "title": "apple-posts-record-india-sales-in-tough-october-december-quarter.jpg", "thumbnailUrl": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTJPj_-gSC3qur68Rz_cx_U05eBVXTpIAh_4aQ8wuyQmbQtQldNflp3Wg" }, { "url": "http://metropolitan.fi/files/2016-07/apple-logo-black.svg.png", "title": "apple-logo-black.svg.png", "thumbnailUrl": "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcTYrLl3OUFL_vwnlVSyv4OQcgIVM7sK0tt04XMSZ09TbzGmTC6NQijg_M0" }, { "url": "http://www.chatswoodchasesydney.com.au/media/13792/apple-logo.jpg.ashx?width=300&height=300&preset=default", "title": "apple-logo.jpg.ashx", "thumbnailUrl": "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcSH6ow51rdLOZlVeR-zqpjsG5vnO0aiZjhjEHfwSC4sQ-8w61lTRaA7TA" }];


    beforeEach(function () {
        module('imageSearchApp');
    });
    

    beforeEach(inject(function (imageSearchService, localStorageService, $httpBackend, $q) {
        ImageSearchService = imageSearchService;
        LocalStorageService = localStorageService;
        http = $httpBackend;

        http.whenGET(/.*/).respond(200, '');
    }));
    

    describe('On initial load', function () {

        var vm;

        beforeEach(inject(function ($controller) {
            spyOn(LocalStorageService, 'getSavedResults');
            vm = $controller('imageSearchController', { 'imageSearchService': ImageSearchService, 'localStorageService': LocalStorageService });
        }));

        it('variables should be defined', function () {
            expect(vm.searchQuery).toEqual('');
            expect(vm.searchResults).toEqual([]);
            expect(vm.searchHistory).toEqual(undefined);
            expect(vm.error).toEqual(undefined);
        });

        it('localStorageService should be called', function () {
            expect(LocalStorageService.getSavedResults).toHaveBeenCalled();
        });

    });


    describe('On history data loaded', function () {

        var vm;

        beforeEach(inject(function ($controller) {
            spyOn(LocalStorageService, 'getSavedResults').and.returnValue(searchHistoryMock);
            vm = $controller('imageSearchController', { 'imageSearchService': ImageSearchService, 'localStorageService': LocalStorageService });
        }));

        it('localStorageService should be called and search history should be populated', function () {
            expect(vm.searchHistory).toEqual(searchHistoryMock);
        });


        it('On restore history click should show proper search result', function () {
            vm.restoreHistory('1471539932146');
            expect(vm.searchResults.length).toEqual(searchHistoryMock[0].searchResults.length);
            expect(vm.searchResults).toEqual(searchHistoryMock[0].searchResults);

            vm.restoreHistory('1471539946746');
            expect(vm.searchResults.length).toEqual(searchHistoryMock[2].searchResults.length);
            expect(vm.searchResults).toEqual(searchHistoryMock[2].searchResults);
        });

    });


    describe('On image search', function () {

        var vm, scope;

        beforeEach(inject(function ($controller, $rootScope, $q) {

            spyOn(LocalStorageService, 'getSavedResults');
            spyOn(LocalStorageService, 'saveResults');
            spyOn(ImageSearchService, 'searchImages').and.callFake(function () {
                var deferred = $q.defer();
                deferred.resolve(searchResultsMock);
                return deferred.promise;
            });
            scope = $rootScope.$new();
            vm = $controller('imageSearchController', { 'imageSearchService': ImageSearchService, 'localStorageService': LocalStorageService });

        }));

        it('if search query specified imageSearchService should be called and search results should be populated', function () {
            vm.searchQuery = 'apple';
            vm.search();

            scope.$apply();

            expect(ImageSearchService.searchImages).toHaveBeenCalled();
            expect(vm.searchResults.length).toEqual(searchResultsMock.length);
            expect(vm.searchResults).toEqual(searchResultsMock);
        });

        it('if search query not specified imageSearchService should not be called and search results should not be populated', function () {
            vm.searchQuery = '';
            vm.search();
            scope.$apply();

            expect(ImageSearchService.searchImages).not.toHaveBeenCalled();
            expect(vm.searchResults.length).toEqual(0);
            expect(vm.searchResults).toEqual([]);
        });


        it('perfom search and then click on remove element, element should be removed from search results', function () {
            vm.searchQuery = 'apple';
            vm.search();
            scope.$apply();
            vm.removeElement(5);
            scope.$apply();

            expect(vm.searchResults.length).toEqual(9);
        });


        it('perfom search and then click save results, localStorageService should be called', function () {
            vm.searchQuery = 'apple';
            vm.search();
            scope.$apply();
            vm.saveSearchResults();
            scope.$apply();

            expect(LocalStorageService.saveResults).toHaveBeenCalled();
            expect(LocalStorageService.getSavedResults).toHaveBeenCalled();
        });

    });

});