(function () {
    'use strict';

    angular
        .module('imageSearchApp')
        .factory('imageSearchService', imageSearchService);

    imageSearchService.$inject = ['$http', '$httpParamSerializer', 'modelTransformer'];

    function imageSearchService($http, $httpParamSerializer, modelTransformer) {

        var service = {
            searchImages: searchImages
        };

        var googleApiUrl = "https://www.googleapis.com/customsearch/v1";
        var googleSearchResult = {};

        function searchImages(queryText) {

            var searchParams = {
                'q': queryText,
                'key': 'AIzaSyDLF4mZZTwcGZO0XoFhXN0Z1cTLSSu9j7s',
                'cx': '010614378049903159039:h3-6gwuk96u',
                'searchType': 'image',
                'imgSize': 'medium',
                'num': 10
            }

            return $http({
                method: 'GET',
                //url: 'tests/image-search/googlesearchdata.json',
                url: googleApiUrl + '?' + $httpParamSerializer(searchParams),
            }).then(function (response) {
                googleSearchResult = modelTransformer.transform(response.data);
                return googleSearchResult;
            });
        }

        return service;
    }

})();