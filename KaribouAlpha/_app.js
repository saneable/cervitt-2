angular
    .module("karibouAlphaApp", ["ui.router", "azure-mobile-service.module", "LocalStorageModule", "flow", "ui.bootstrap"])
    .constant("ngAuthSettings", {
        apiServiceBaseUri: "/",
        clientId: "kariboualpha"
    })
    .constant("AzureMobileServiceClient", {
        API_URL: "/",
        API_KEY: "gkwGJioLD3jNxrAX6krXh6jVk6SFkeQr"
    })
    .constant("Countries",
        [
        { name: "Afghanistan", code: "AF" },
        { name: "Åland Islands", code: "AX" },
        { name: "Albania", code: "AL" },
        { name: "Algeria", code: "DZ" },
        { name: "American Samoa", code: "AS" },
        { name: "AndorrA", code: "AD" },
        { name: "Angola", code: "AO" },
        { name: "Anguilla", code: "AI" },
        { name: "Antarctica", code: "AQ" },
        { name: "Antigua and Barbuda", code: "AG" },
        { name: "Argentina", code: "AR" },
        { name: "Armenia", code: "AM" },
        { name: "Aruba", code: "AW" },
        { name: "Australia", code: "AU" },
        { name: "Austria", code: "AT" },
        { name: "Azerbaijan", code: "AZ" },
        { name: "Bahamas", code: "BS" },
        { name: "Bahrain", code: "BH" },
        { name: "Bangladesh", code: "BD" },
        { name: "Barbados", code: "BB" },
        { name: "Belarus", code: "BY" },
        { name: "Belgium", code: "BE" },
        { name: "Belize", code: "BZ" },
        { name: "Benin", code: "BJ" },
        { name: "Bermuda", code: "BM" },
        { name: "Bhutan", code: "BT" },
        { name: "Bolivia", code: "BO" },
        { name: "Bosnia and Herzegovina", code: "BA" },
        { name: "Botswana", code: "BW" },
        { name: "Bouvet Island", code: "BV" },
        { name: "Brazil", code: "BR" },
        { name: "British Indian Ocean Territory", code: "IO" },
        { name: "Brunei Darussalam", code: "BN" },
        { name: "Bulgaria", code: "BG" },
        { name: "Burkina Faso", code: "BF" },
        { name: "Burundi", code: "BI" },
        { name: "Cambodia", code: "KH" },
        { name: "Cameroon", code: "CM" },
        { name: "Canada", code: "CA" },
        { name: "Cape Verde", code: "CV" },
        { name: "Cayman Islands", code: "KY" },
        { name: "Central African Republic", code: "CF" },
        { name: "Chad", code: "TD" },
        { name: "Chile", code: "CL" },
        { name: "China", code: "CN" },
        { name: "Christmas Island", code: "CX" },
        { name: "Cocos (Keeling) Islands", code: "CC" },
        { name: "Colombia", code: "CO" },
        { name: "Comoros", code: "KM" },
        { name: "Congo", code: "CG" },
        { name: "Congo, The Democratic Republic of the", code: "CD" },
        { name: "Cook Islands", code: "CK" },
        { name: "Costa Rica", code: "CR" },
        { name: "Cote D\"Ivoire", code: "CI" },
        { name: "Croatia", code: "HR" },
        { name: "Cuba", code: "CU" },
        { name: "Cyprus", code: "CY" },
        { name: "Czech Republic", code: "CZ" },
        { name: "Denmark", code: "DK" },
        { name: "Djibouti", code: "DJ" },
        { name: "Dominica", code: "DM" },
        { name: "Dominican Republic", code: "DO" },
        { name: "Ecuador", code: "EC" },
        { name: "Egypt", code: "EG" },
        { name: "El Salvador", code: "SV" },
        { name: "Equatorial Guinea", code: "GQ" },
        { name: "Eritrea", code: "ER" },
        { name: "Estonia", code: "EE" },
        { name: "Ethiopia", code: "ET" },
        { name: "Falkland Islands (Malvinas)", code: "FK" },
        { name: "Faroe Islands", code: "FO" },
        { name: "Fiji", code: "FJ" },
        { name: "Finland", code: "FI" },
        { name: "France", code: "FR" },
        { name: "French Guiana", code: "GF" },
        { name: "French Polynesia", code: "PF" },
        { name: "French Southern Territories", code: "TF" },
        { name: "Gabon", code: "GA" },
        { name: "Gambia", code: "GM" },
        { name: "Georgia", code: "GE" },
        { name: "Germany", code: "DE" },
        { name: "Ghana", code: "GH" },
        { name: "Gibraltar", code: "GI" },
        { name: "Greece", code: "GR" },
        { name: "Greenland", code: "GL" },
        { name: "Grenada", code: "GD" },
        { name: "Guadeloupe", code: "GP" },
        { name: "Guam", code: "GU" },
        { name: "Guatemala", code: "GT" },
        { name: "Guernsey", code: "GG" },
        { name: "Guinea", code: "GN" },
        { name: "Guinea-Bissau", code: "GW" },
        { name: "Guyana", code: "GY" },
        { name: "Haiti", code: "HT" },
        { name: "Heard Island and Mcdonald Islands", code: "HM" },
        { name: "Holy See (Vatican City State)", code: "VA" },
        { name: "Honduras", code: "HN" },
        { name: "Hong Kong", code: "HK" },
        { name: "Hungary", code: "HU" },
        { name: "Iceland", code: "IS" },
        { name: "India", code: "IN" },
        { name: "Indonesia", code: "ID" },
        { name: "Iran, Islamic Republic Of", code: "IR" },
        { name: "Iraq", code: "IQ" },
        { name: "Ireland", code: "IE" },
        { name: "Isle of Man", code: "IM" },
        { name: "Israel", code: "IL" },
        { name: "Italy", code: "IT" },
        { name: "Jamaica", code: "JM" },
        { name: "Japan", code: "JP" },
        { name: "Jersey", code: "JE" },
        { name: "Jordan", code: "JO" },
        { name: "Kazakhstan", code: "KZ" },
        { name: "Kenya", code: "KE" },
        { name: "Kiribati", code: "KI" },
        { name: "Korea, Democratic People\"S Republic of", code: "KP" },
        { name: "Korea, Republic of", code: "KR" },
        { name: "Kuwait", code: "KW" },
        { name: "Kyrgyzstan", code: "KG" },
        { name: "Lao People\"S Democratic Republic", code: "LA" },
        { name: "Latvia", code: "LV" },
        { name: "Lebanon", code: "LB" },
        { name: "Lesotho", code: "LS" },
        { name: "Liberia", code: "LR" },
        { name: "Libyan Arab Jamahiriya", code: "LY" },
        { name: "Liechtenstein", code: "LI" },
        { name: "Lithuania", code: "LT" },
        { name: "Luxembourg", code: "LU" },
        { name: "Macao", code: "MO" },
        { name: "Macedonia, The Former Yugoslav Republic of", code: "MK" },
        { name: "Madagascar", code: "MG" },
        { name: "Malawi", code: "MW" },
        { name: "Malaysia", code: "MY" },
        { name: "Maldives", code: "MV" },
        { name: "Mali", code: "ML" },
        { name: "Malta", code: "MT" },
        { name: "Marshall Islands", code: "MH" },
        { name: "Martinique", code: "MQ" },
        { name: "Mauritania", code: "MR" },
        { name: "Mauritius", code: "MU" },
        { name: "Mayotte", code: "YT" },
        { name: "Mexico", code: "MX" },
        { name: "Micronesia, Federated States of", code: "FM" },
        { name: "Moldova, Republic of", code: "MD" },
        { name: "Monaco", code: "MC" },
        { name: "Mongolia", code: "MN" },
        { name: "Montserrat", code: "MS" },
        { name: "Morocco", code: "MA" },
        { name: "Mozambique", code: "MZ" },
        { name: "Myanmar", code: "MM" },
        { name: "Namibia", code: "NA" },
        { name: "Nauru", code: "NR" },
        { name: "Nepal", code: "NP" },
        { name: "Netherlands", code: "NL" },
        { name: "Netherlands Antilles", code: "AN" },
        { name: "New Caledonia", code: "NC" },
        { name: "New Zealand", code: "NZ" },
        { name: "Nicaragua", code: "NI" },
        { name: "Niger", code: "NE" },
        { name: "Nigeria", code: "NG" },
        { name: "Niue", code: "NU" },
        { name: "Norfolk Island", code: "NF" },
        { name: "Northern Mariana Islands", code: "MP" },
        { name: "Norway", code: "NO" },
        { name: "Oman", code: "OM" },
        { name: "Pakistan", code: "PK" },
        { name: "Palau", code: "PW" },
        { name: "Palestinian Territory, Occupied", code: "PS" },
        { name: "Panama", code: "PA" },
        { name: "Papua New Guinea", code: "PG" },
        { name: "Paraguay", code: "PY" },
        { name: "Peru", code: "PE" },
        { name: "Philippines", code: "PH" },
        { name: "Pitcairn", code: "PN" },
        { name: "Poland", code: "PL" },
        { name: "Portugal", code: "PT" },
        { name: "Puerto Rico", code: "PR" },
        { name: "Qatar", code: "QA" },
        { name: "Reunion", code: "RE" },
        { name: "Romania", code: "RO" },
        { name: "Russian Federation", code: "RU" },
        { name: "RWANDA", code: "RW" },
        { name: "Saint Helena", code: "SH" },
        { name: "Saint Kitts and Nevis", code: "KN" },
        { name: "Saint Lucia", code: "LC" },
        { name: "Saint Pierre and Miquelon", code: "PM" },
        { name: "Saint Vincent and the Grenadines", code: "VC" },
        { name: "Samoa", code: "WS" },
        { name: "San Marino", code: "SM" },
        { name: "Sao Tome and Principe", code: "ST" },
        { name: "Saudi Arabia", code: "SA" },
        { name: "Senegal", code: "SN" },
        { name: "Serbia and Montenegro", code: "CS" },
        { name: "Seychelles", code: "SC" },
        { name: "Sierra Leone", code: "SL" },
        { name: "Singapore", code: "SG" },
        { name: "Slovakia", code: "SK" },
        { name: "Slovenia", code: "SI" },
        { name: "Solomon Islands", code: "SB" },
        { name: "Somalia", code: "SO" },
        { name: "South Africa", code: "ZA" },
        { name: "South Georgia and the South Sandwich Islands", code: "GS" },
        { name: "Spain", code: "ES" },
        { name: "Sri Lanka", code: "LK" },
        { name: "Sudan", code: "SD" },
        { name: "Suriname", code: "SR" },
        { name: "Svalbard and Jan Mayen", code: "SJ" },
        { name: "Swaziland", code: "SZ" },
        { name: "Sweden", code: "SE" },
        { name: "Switzerland", code: "CH" },
        { name: "Syrian Arab Republic", code: "SY" },
        { name: "Taiwan, Province of China", code: "TW" },
        { name: "Tajikistan", code: "TJ" },
        { name: "Tanzania, United Republic of", code: "TZ" },
        { name: "Thailand", code: "TH" },
        { name: "Timor-Leste", code: "TL" },
        { name: "Togo", code: "TG" },
        { name: "Tokelau", code: "TK" },
        { name: "Tonga", code: "TO" },
        { name: "Trinidad and Tobago", code: "TT" },
        { name: "Tunisia", code: "TN" },
        { name: "Turkey", code: "TR" },
        { name: "Turkmenistan", code: "TM" },
        { name: "Turks and Caicos Islands", code: "TC" },
        { name: "Tuvalu", code: "TV" },
        { name: "Uganda", code: "UG" },
        { name: "Ukraine", code: "UA" },
        { name: "United Arab Emirates", code: "AE" },
        { name: "United Kingdom", code: "GB" },
        { name: "United States", code: "US" },
        { name: "United States Minor Outlying Islands", code: "UM" },
        { name: "Uruguay", code: "UY" },
        { name: "Uzbekistan", code: "UZ" },
        { name: "Vanuatu", code: "VU" },
        { name: "Venezuela", code: "VE" },
        { name: "Viet Nam", code: "VN" },
        { name: "Virgin Islands, British", code: "VG" },
        { name: "Virgin Islands, U.S.", code: "VI" },
        { name: "Wallis and Futuna", code: "WF" },
        { name: "Western Sahara", code: "EH" },
        { name: "Yemen", code: "YE" },
        { name: "Zambia", code: "ZM" },
        { name: "Zimbabwe", code: "ZW" }
        ]
    )
    .constant("ProductFileCategories",
        [
        { name: "Roadmap", code: 0 },
        { name: "Whitepaper", code: 1 },
        { name: "Flash post", code: 2 },
        { name: "Teaser material", code: 3 },
        { name: "Value proposition / ROI calculator / Business case", code: 4 },
        { name: "Case study", code: 5 },
        { name: "Demo / sandbox ", code: 6 },
        { name: "Webinar", code: 7 },
        { name: "Project plans release & GTM management", code: 8 },
        { name: "Strategy paper", code: 9 },
        { name: "Competitor comparison", code: 10 },
        { name: "Training material", code: 11 },
        { name: "Pricing sheet / rate card / discounting", code: 12 },
        { name: "Knowledge share / Legal guidelines", code: 13 },
        { name: "Contact us", code: 14 },
        ]
    )
    .constant("ProductUpdateTypes",
        {
            ProductAdded: 0,
            ProductEdited: 1,
            ProductDeleted: 2,
            ProductFileAdded: 3,
            ProductFileEdited: 4,
            ProductFileDeleted: 5,
            ProductCustomerAdded: 6,
            ProductCustomerDeleted: 7
        }
    )
    .constant("ProductPrivacyEnum",
        [
        { name: "Public", code: 0 },
        { name: "Private", code: 1 },
        { name: "Selected groups", code: 3 },
        ]
    )
    .constant("ProductFilePrivacyEnum",
        [
        { name: "Public", code: 0 },
        { name: "Private", code: 1 },
        { name: "Selected groups", code: 3 },
        ]
    )
    
    .config(["$compileProvider", function ($compileProvider) {
        $compileProvider.debugInfoEnabled(false);
    }])
    .config(["$httpProvider", function ($httpProvider) {
        $httpProvider.interceptors.push("AuthenticationInterceptorService");
    }])
    .config(["$locationProvider", function ($locationProvider) {
        $locationProvider.html5Mode(true);
    }])
    .filter("filterCompanyConnection", function () {
        return function (companyConnections, searchString) {
            var results = [];

            if (!searchString || searchString.length === 0) {
                return companyConnections;
            }

            searchString = searchString.toLowerCase();

            for (var i = 0, len = companyConnections.length; i < len; i++) {
                if (companyConnections[i].CompanyDisplayName && companyConnections[i].CompanyDisplayName.toLowerCase().indexOf(searchString) >= 0) {
                    results.push(companyConnections[i]);
                }
            }

            return results;
        };
    })
    .filter("filterPersonalConnection", function () {
        return function (personalConnections, searchString) {
            var results = [];

            if (!searchString || searchString.length === 0) {
                return personalConnections;
            }

            searchString = searchString.toLowerCase();

            for (var i = 0, len = personalConnections.length; i < len; i++) {
                if (personalConnections[i].UserUserName && personalConnections[i].UserUserName.toLowerCase().indexOf(searchString) >= 0) {
                    results.push(personalConnections[i]);

                    continue;
                }

                if (personalConnections[i].UserLastName && personalConnections[i].UserLastName.toLowerCase().indexOf(searchString) >= 0) {
                    results.push(personalConnections[i]);

                    continue;
                }

                if (personalConnections[i].UserFirstName && personalConnections[i].UserFirstName.toLowerCase().indexOf(searchString) >= 0) {
                    results.push(personalConnections[i]);

                    continue;
                }

                if (personalConnections[i].UserCompanyDisplayName && personalConnections[i].UserCompanyDisplayName.toLowerCase().indexOf(searchString) >= 0) {
                    results.push(personalConnections[i]);

                    continue;
                }

                if (personalConnections[i].UserJobTitle && personalConnections[i].UserJobTitle.toLowerCase().indexOf(searchString) >= 0) {
                    results.push(personalConnections[i]);

                    continue;
                }
            }

            return results;
        };
    })
    .factory("AuthenticationService", ["$http", "$q", "localStorageService", "ngAuthSettings", function ($http, $q, localStorageService, ngAuthSettings) {
        //http://bitoftech.net/2014/08/11/asp-net-web-api-2-external-logins-social-logins-facebook-google-angularjs-app/comment-page-1/
        var serviceBase = ngAuthSettings.apiServiceBaseUri;
        var authenticationServiceFactory = {};
        var _authentication = {
            isAuth: false,
            userName: "",
            useRefreshTokens: false
        };
        var _externalAuthData = {
            provider: "",
            userName: "",
            externalAccessToken: ""
        };

        var _saveRegistration = function (registration) {
            _logOut();

            //return $http.post(serviceBase + "api/account/register", registration)
            //    .then(function (response) {
            //        return response;
            //    }
            //);
            var response = $http({
                method: "post",
                url: "api/account/register",
                data: JSON.stringify(registration),
                dataType: "json"
            });
            return response;
        };

        var _login = function (loginData) {

            var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

            if (loginData.useRefreshTokens) {
                data = data + "&client_id=" + ngAuthSettings.clientId;
            }

            var deferred = $q.defer();

            $http.post(serviceBase + "token", data, { headers: { "Content-Type": "application/x-www-form-urlencoded" } })
                .success(function (response) {
                    if (loginData.useRefreshTokens) {
                        localStorageService.set("authorizationData", { token: response.access_token, tokenExpirationDate: response[".expires"], userName: loginData.userName, refreshToken: response.refresh_token, useRefreshTokens: true });
                    }
                    else {
                        localStorageService.set("authorizationData", { token: response.access_token, tokenExpirationDate: response[".expires"], userName: loginData.userName, refreshToken: "", useRefreshTokens: false });
                    }

                    _authentication.isAuth = true;
                    _authentication.userName = loginData.userName;
                    _authentication.useRefreshTokens = loginData.useRefreshTokens;
                    deferred.resolve(response);
                })
                .error(function (err, status) {
                    _logOut();
                    deferred.reject(err);
                });

            return deferred.promise;
        };

        var _logOut = function () {
            
            localStorageService.remove("authorizationData");
            _authentication.isAuth = false;
            _authentication.userName = "";
            _authentication.useRefreshTokens = false;
        };

        var _fillAuthData = function () {
            var authData = localStorageService.get("authorizationData");

            if (authData) {
                _authentication.isAuth = true;
                _authentication.userName = authData.userName;
                _authentication.useRefreshTokens = authData.useRefreshTokens;
            }
        };

        var _refreshToken = function () {
            var deferred = $q.defer();
            var authData = localStorageService.get("authorizationData");

            if (authData) {
                if (authData.useRefreshTokens === true) {
                    var data = "grant_type=refresh_token&refresh_token=" + authData.refreshToken + "&client_id=" + ngAuthSettings.clientId;

                    localStorageService.remove("authorizationData");
                    $http.post(serviceBase + "token", data, { headers: { "Content-Type": "application/x-www-form-urlencoded" } })
                        .success(function (response) {
                            localStorageService.set("authorizationData", { token: response.access_token, userName: response.userName, refreshToken: response.refresh_token, useRefreshTokens: true });
                            deferred.resolve(response);
                        }
                    ).error(function (err, status) {
                        _logOut();
                        deferred.reject(err);
                    });
                }
            }

            return deferred.promise;
        };

        var _obtainAccessToken = function (externalData) {
            var deferred = $q.defer();
            $http.get(serviceBase + "api/account/ObtainLocalAccessToken", { params: { provider: externalData.provider, externalAccessToken: externalData.externalAccessToken } })
                .success(function (response) {
                    localStorageService.set("authorizationData", { token: response.access_token, userName: response.userName, refreshToken: "", useRefreshTokens: false });
                    _authentication.isAuth = true;
                    _authentication.userName = response.userName;
                    _authentication.useRefreshTokens = false;
                    deferred.resolve(response);
                }
            ).error(function (err, status) {
                _logOut();
                deferred.reject(err);
            });

            return deferred.promise;
        };

        var _registerExternal = function (registerExternalData) {
            var deferred = $q.defer();

            $http.post(serviceBase + "api/account/registerexternal", registerExternalData)
                .success(function (response) {
                    localStorageService.set("authorizationData", { token: response.access_token, userName: response.userName, refreshToken: "", useRefreshTokens: false });
                    _authentication.isAuth = true;
                    _authentication.userName = response.userName;
                    _authentication.useRefreshTokens = false;
                    deferred.resolve(response);
                }
            ).error(function (err, status) {
                _logOut();
                deferred.reject(err);
            });

            return deferred.promise;
        };

        authenticationServiceFactory.saveRegistration = _saveRegistration;
        authenticationServiceFactory.login = _login;
        authenticationServiceFactory.logOut = _logOut;
        authenticationServiceFactory.fillAuthData = _fillAuthData;
        authenticationServiceFactory.authentication = _authentication;
        authenticationServiceFactory.refreshToken = _refreshToken;
        authenticationServiceFactory.obtainAccessToken = _obtainAccessToken;
        authenticationServiceFactory.externalAuthData = _externalAuthData;
        authenticationServiceFactory.registerExternal = _registerExternal;

        return authenticationServiceFactory;
    }])
    .factory("AuthenticationInterceptorService", ["$q", "$injector", "$location", "localStorageService", function ($q, $injector, $location, localStorageService) {
        var authenticationInterceptorServiceFactory = {};
        var _request = function (config) {
            config.headers = config.headers || {};

            var authData = localStorageService.get("authorizationData");

            if (authData) {
                config.headers.Authorization = "Bearer " + authData.token;
            }

            return config;
        };

        var _responseError = function (rejection) {
            if (rejection.status === 401) {
                var authenticationService = $injector.get("AuthenticationService");
                var authorizationData = localStorageService.get("authorizationData");

                if (authorizationData) {
                    if (authorizationData.useRefreshTokens) {
                        $location.path("/refresh");

                        return $q.reject(rejection);
                    }
                }

                authenticationService.logOut();
                $location.path("/login");
            }

            return $q.reject(rejection);
        };

        authenticationInterceptorServiceFactory.request = _request;
        authenticationInterceptorServiceFactory.responseError = _responseError;

        return authenticationInterceptorServiceFactory;
    }])
    .factory("TokensManagerService", ["$http", "ngAuthSettings", function ($http, ngAuthSettings) {
        var serviceBase = ngAuthSettings.apiServiceBaseUri;
        var tokenManagerServiceFactory = {};

        var _getRefreshTokens = function () {
            return $http.get(serviceBase + "api/refreshtokens").then(function (results) {
                return results;
            });
        };

        var _deleteRefreshTokens = function (tokenid) {
            return $http.delete(serviceBase + "api/refreshtokens/?tokenid=" + tokenid)
                .then(function (results) {
                    return results;
                }
            );
        };

        tokenManagerServiceFactory.deleteRefreshTokens = _deleteRefreshTokens;
        tokenManagerServiceFactory.getRefreshTokens = _getRefreshTokens;

        return tokenManagerServiceFactory;
    }])
    .factory("ForgotPasswordService", ["$http", "$q", "localStorageService", "ngAuthSettings", function ($http, $q, localStorageService, ngAuthSettings) {

        var serviceBase = ngAuthSettings.apiServiceBaseUri;
        var authenticationServiceFactory = {};
        var _authentication = {
            isAuth: false,
            userName: "",
            useRefreshTokens: false
        };



        var _ForgotPassword = function (email) {
            var response = $http({
                method: "post",
                url: "api/account/forgotPassword",
                data: JSON.stringify(email),
                dataType: "json",
                headers: { "Content-Type": "application/json" }
            });
            return response;
        };

        var _CheckCode = function (Code) {
            
            var response = $http({
                method: "post",
                url: "api/account/CheckCode",
                data: JSON.stringify(Code),
                dataType: "json",
                headers: { "Content-Type": "application/json" }
            });
            
            return response;
        };

        var _ResetPassword = function (id, token, Password) {

            var Model = {
                NewPassword: Password,
                UserID: id,
                Token: token,
                NewPasswordConfirmation: Password
            }
          
            var response = $http({
                method: "post",
                url: "api/account/ResetPassword",
                data: JSON.stringify(Model),
                dataType: "json",
                headers: { "Content-Type": "application/json" }
            });
            return response;
        };

        authenticationServiceFactory.ForgotPassword = _ForgotPassword;
        authenticationServiceFactory.CheckCode = _CheckCode;
        authenticationServiceFactory.ResetPassword = _ResetPassword;

        return authenticationServiceFactory;
    }])
     .service("TransferData", function () {

         var UserWithToken = {};
         var UserData;
         var UserToken;
         var Skills;

         var AddUserData = function (Id) {
             UserData = Id;
         };

         var AddUserAndTokenData = function (Id, Token) {
             UserData = Id;
             UserToken = Token
         };

         var GetUserData = function () {
             return UserData;
         };

         var GetUserWithToken = function () {
             UserWithToken.Id = UserData;
             UserWithToken.Token = UserToken;
             return UserWithToken;
         };

         var _AddSkillsData = function (skills)
         {
             Skills = skills;
         }

         var _GetSkillsData = function () {
            
             // return _AddSkillsData();
             return Skills;
         }

         return {
             AddUserAndTokenData: AddUserAndTokenData,
             AddUserData: AddUserData,
             GetUserData: GetUserData,
             AddSkillsData: _AddSkillsData,
             GetSkillsData: _GetSkillsData,
             GetUserWithToken: GetUserWithToken
         };

         authenticationServiceFactory.AddUserData = _AddUserData;
         authenticationServiceFactory.GetUserData = _GetUserData;
         authenticationServiceFactory.AddSkillsData = _AddSkillsData;
         authenticationServiceFactory.GetSkillsData = _GetSkillsData;
     })
    .factory("SectorService", ["$http", "localStorageService", "ngAuthSettings", function ($http, localStorageService, ngAuthSettings) {
        var serviceBase = ngAuthSettings.apiServiceBaseUri;
        var authenticationServiceFactory = {};
        var _authentication = {
            isAuth: true,
            userName: "",
            useRefreshTokens: false
        };

        var _AddNewSector = function (SectorName) {
            var newSectorDTO = { SectorName: SectorName };


            var response = $http({
                method: "post",
                url: "api/NewSector",
                data: JSON.stringify(newSectorDTO),
                dataType: "json",
                headers: { "Content-Type": "application/json" }
            });
            return response;
        };

        var _GetSector = function () {



            var response = $http({
                method: "get",
                url: "api/GetSectors",
                dataType: "json",
                headers: { "Content-Type": "application/json" }
            });
            return response;
        };



        authenticationServiceFactory.AddNewSector = _AddNewSector;
        authenticationServiceFactory.GetSector = _GetSector;
        return authenticationServiceFactory;
    }])

    .factory("InviteExistingUser", ["$http", "localStorageService", "ngAuthSettings", function ($http, localStorageService, ngAuthSettings) {
        var serviceBase = ngAuthSettings.apiServiceBaseUri;
        var authenticationServiceFactory = {};
        var _authentication = {
            isAuth: true,
            userName: "",
            useRefreshTokens: false
        };

        debugger

        var _InviteUser = function (Id) {
            debugger
            var response = $http({
                method: "post",
                url: "api/InviteToJoinMyCompany",
                data: JSON.stringify(Id),
                dataType: "json",
                headers: { "Content-Type": "application/json" }
            });
            return response;
        };



        
        authenticationServiceFactory.InviteUser = _InviteUser;
        return authenticationServiceFactory;
    }])


    .factory("skillListData", ["$http", "localStorageService", "ngAuthSettings", function ($http, localStorageService, ngAuthSettings) {

        var response = $http({
            method: "get",
            url: "api/GetSkills",
            dataType: "json",
            headers: { "Content-Type": "application/json" }
        });
        return response;
    }])

    .directive('tagManager', function (skillListData, TransferData) {
        
        return {
            restrict: 'E',
            scope: { tags: '=' },
            template:
                '<div class="row">'+
                          '<label class="col-sm-4 account-company-details mandatory">Core services</label>'+
                        
                            
                '<div class="col-sm-5 account-company-details"><input type="text" typeahead-on-select="onSelectSkills($item, $model, $label, $event)" uib-typeahead="skl.SkillName for skl in skills | filter:$viewValue" class="form-control account-company-details"  placeholder="Type to add core service" ng-model="new_value"></input><span class="alert-danger-new" ng-show=skillNewValueRequired>Please enter core service.</span><span class="alert-danger-new" ng-show=UniqueSkillError>Please enter unique core service.</span> ' +
                '<div class="tags custom_tags row pull-left">' +
                '<div class="col-md-12"><a ng-repeat="(idx, tag) in tags"  href="javascript:void(0)" class="tag close-box" ng-click="remove(idx)">{{tag}}</a></div>' +
                '</div>'+
                '</div>' +

                '<div class="col-sm-2 account-company-details"><a class="onboarding-start" ng-click="add()" title="Click to Save New Core Service" href="javascript:void(0)" ><span><i class="fa fa-plus"></i>&nbsp;Add</span></a></div></div>',
            
            link:{ 
                         
                
                post: function ($scope, $element, $rootScope) {

            // FIXME: this is lazy and error-prone
                    var input = angular.element($element.children()[1]);
        $scope.skills = TransferData.GetSkillsData();
       
        //var data = $rootScope.CoreServices;
        // This adds the new tag to the tags array
        $scope.add = function () {
            
            var value = $scope.Services;
            $scope.skillNewValueRequired = false;
            $scope.UniqueSkillError = false;

            if ($scope.new_value == undefined || $scope.new_value == "") {
                $scope.new_value = ""
                $scope.skillNewValueRequired = true;
                $scope.UniqueSkillError = false;
            }
            else if ($scope.tags.indexOf($scope.new_value) == -1) {
                var isFound = false;
                angular.forEach(skillListData.$$state.value.data, function (value, key) {
                    if (isFound == false) {
                        isFound = angular.equals(value.SkillName.toLowerCase(), $scope.new_value.toLowerCase());
                    }
                });
                var isFoundLocal = false;

                for (var i = 0; i < $scope.tags.length; i++) {
                    if (isFoundLocal == false) {
                        isFoundLocal = angular.equals($scope.tags[i].toLowerCase(), $scope.new_value.toLowerCase());
                    }
                }
                if (!isFound && !isFoundLocal) {
                    $scope.tags.push($scope.new_value);
                    $scope.new_value = "";
                }
            };


            // This is the ng-click handler to remove an item
           

            // Capture all keypresses
            input.bind('keypress', function (event) {
                
                // But we only care when Enter was pressed
                if (event.keyCode == 13) {
                    // There's probably a better way to handle this...
                    $scope.$apply($scope.add);
                }
            });

            function handle(e) {
                
                if (e.keyCode === 13) {
                    e.preventDefault(); // Ensure it is only this code that rusn
                    return false;
                }
            }
        }

        $scope.remove = function (idx) {
            
            $scope.tags.splice(idx, 1);
        };

        $scope.onSelectSkills = function ($item, $model, $label, $event) {
            debugger
            
            if ($scope.tags.indexOf($item.SkillName) == -1) {
                $scope.tags.push($item.SkillName);
                $scope._selectedCoreService = "";
                $scope.new_value = "";
            }
            else {
                $scope._selectedCoreService = "";
                $scope.new_value = "";
            }
        };

                    
    }
            
}
    }
})
    .directive('restrictField', function () {
        return {
            restrict: 'AE',
            scope: {
                restrictField: '='
            },
            link: function (scope) {
                // this will match spaces, tabs, line feeds etc
                // you can change this regex as you want
                var regex = /\s/g;

                scope.$watch('restrictField', function (newValue, oldValue) {
                    if (newValue != oldValue && regex.test(newValue)) {
                        scope.restrictField = newValue.replace(regex, '');
                    }
                });
            }
        };
    })

    .directive('editableSelect', function (localStorageService, Azureservice) {
        return {

            
            restrict: 'E',
            replace: true,
            templateUrl: 'onboardingSearchResultsTypeaheadTemplate2.html',
            link: function ($scope, $element) {

                $scope.CompanyShow = false;
                
                $scope.search = function (searchQuery) {
                    
                    var authData = localStorageService.get("authorizationData");
                     Azureservice.invokeApi("SearchCompanies?query=" + encodeURIComponent(searchQuery) + "&maxRecords=10", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
                     .then(function (companies) {
                         $scope.CompanyData = companies;
                         console.log($scope.CompanyData);
                         $scope.CompanyShow = true;
                     });
                     
                };

                $scope.onSelect = function ($model) {
                    console.log("this is selected..");
                    $scope.CompanyModel.Name = $model.DisplayName;
                    $scope.CompanyModel.ID = $model.ID;
                    $scope.CompanyShow = false;

                    //$scope.searchQuery = $model.DisplayName;
                    //$scope.selectedQueryCompanyId = $model.ID;
                };
               
               

                

            }


        };
    }) 
    
.controller("loginController", ["$scope", "$location", "$state", "AuthenticationService", "ngAuthSettings", "$rootScope", "localStorageService", "Azureservice", function ($scope, $location, $state, AuthenticationService, ngAuthSettings, $rootScope, localStorageService, Azureservice) {

    
    var controller = this;

    controller.loginData = {
        userName: "",
        password: "",
        useRefreshTokens: false
    };
    $scope.errorMessage = "Email or Password is not valid.";
    controller.hasError = false;
    controller.busy = false;
    controller.toState = $rootScope.toState;
    controller.toParams = $rootScope.toParams;
    $rootScope.toState = null;
    $rootScope.toParams = null;
    $scope.LoginError = false;

    $scope.emailRequiredError = false;
    $scope.passwordRequiredError = false;

    controller.cancel = $scope.$dismiss;

    controller.login = function () {
        $scope.emailRequiredError = false;
        $scope.passwordRequiredError = false;
        $scope.LoginError = false;

        
        if ((controller.loginData.userName == "undefined" || controller.loginData.userName == "") && controller.loginData.password == "") {
            $scope.emailRequiredError = false;
            $scope.passwordRequiredError = false;

        }
        else {
            if ($("#pre-header").hasClass("login_active") && (controller.loginData.userName == "undefined" || controller.loginData.userName == "")) {
                $scope.emailRequiredError = true;
            }
            if ($("#pre-header").hasClass("login_active") && controller.loginData.password == "") {
                $scope.passwordRequiredError = true;
            }
        }
        if ($("#pre-header").hasClass("login_active") && (controller.loginData.userName != null && controller.loginData.userName != "" && controller.loginData.password)) {

            AuthenticationService.login(controller.loginData)

           .then(function (response) {
               if (controller.toState && controller.toParams) {
                   $state.go(controller.toState, controller.toParams);
                   return;
               }

               if (controller.toState) {
                   $state.go(controller.toState);
                   return;
               }

               var authData = localStorageService.get("authorizationData");

               Azureservice.invokeApi("GetMyOnboardingStatus", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
                 .then(function (onboardingStatus) {
                     if (onboardingStatus.OnboardingSkipped) {
                         $state.go("dashboard");

                         return;
                     }

                     if (!onboardingStatus.OnboardingStep) {
                         $state.go("onboarding");

                         return;
                     }

                     if (onboardingStatus.OnboardingStep === 1) {
                         $state.go("onboardingStepOne");

                         return;
                     }

                     if (onboardingStatus.OnboardingStep === 2) {
                         $state.go("onboardingStepTwo");

                         return;
                     }

                     if (onboardingStatus.OnboardingStep === 3) {
                         $state.go("onboardingStepThree");

                         return;
                     }

                     if (onboardingStatus.OnboardingStep === 4) {
                         $state.go("onboardingStepFour");

                         return;
                     }

                     $state.go("dashboard");
                 });

               return;
           },
            function (err) {
                if (err) {

                    $scope.errorMessage = err.error_description;
                    $('#pre-header').addClass('login_active');
                }
                controller.hasError = true;
                $scope.LoginError = true;
            })
           .finally(function () {
               controller.busy = false;
           });
        }
    };

    controller.signup = function () {
        $rootScope.$emit("ShouldSignupEvent");
        $scope.$dismiss();
    };

    controller.forgotPassword = function () {
        $rootScope.$emit("ForgotPasswordEvent");
        $scope.$dismiss();
    };

    controller.facebookLogin = function () {
        var redirectUri = location.protocol + "//" + location.host + "/facebook_authentication_complete.html";
        var externalProviderUrl = ngAuthSettings.apiServiceBaseUri + "api/Account/ExternalLogin?provider=Facebook"
                                                                    + "&response_type=token&client_id=" + ngAuthSettings.clientId
                                                                    + "&redirect_uri=" + redirectUri;
        window.$windowScope = controller;

        var oauthWindow = window.open(externalProviderUrl, "Authenticate Account", "location=0,status=0,width=600,height=750");
    };

    controller.googleLogin = function () {
        alert("google login");
        var redirectUri = location.protocol + '//' + location.host + '/authcomplete.html';
        var externalProviderUrl = location.protocol + '//' + location.host + "/api/Account/ExternalLogin?provider=Google";
        window.$windowScope = controller;
        alert(externalProviderUrl);
        var oauthWindow = window.open(externalProviderUrl, "Authenticate Account", "location=0,status=0,width=600,height=750");
    };
    
    $scope.checkKeyPressEvent = function (keyEvent) {
        $scope.emailRequiredError = false;
        $scope.errorEmailInvalid = false;
        console.log("checkKeyPressEvent");

    }

    controller.facebookAuthenticationCompletedCallback = function (fragment) {
        $scope.$apply(function () {
            var externalData = {
                provider: fragment.provider,
                externalAccessToken: fragment.external_access_token
            };

            if (fragment.haslocalaccount === "False") {
                AuthenticationService.logOut();

                var registerData = {
                    provider: fragment.provider,
                    userName: fragment.external_user_name,
                    externalAccessToken: fragment.external_access_token
                };

                AuthenticationService.registerExternal(registerData)
                    .then(function (response) {
                        AuthenticationService.obtainAccessToken(externalData)
                        .then(function (response) {
                            $scope.$close(response.userName);
                        },
                     function (err) {
                         controller.errorMessage = err.error_description;
                         controller.hasError = true;
                     });
                    },
                 function (err) {
                     controller.errorMessage = err.error_description;
                     controller.hasError = true;
                 });
            }
            else {
                AuthenticationService.obtainAccessToken(externalData)
                    .then(function (response) {
                        $scope.$close(response.userName);
                    },
                 function (err) {
                     controller.errorMessage = err.error_description;
                     controller.hasError = true;
                 });
            }
        });
    };
}])
.controller("LogouController", ["$scope", "$location", "AuthenticationService", function ($scope, $location, AuthenticationService) {
    
    AuthenticationService.logOut();
    $location.path("/login");

}])
.controller("ForgotPasswordController", ["$scope", "$location", "$timeout", "AuthenticationService", "ForgotPasswordService", "$stateParams", "$state", "TransferData", function ($scope, $location, $timeout, AuthenticationService, ForgotPasswordService, $stateParams, $state, TransferData) {


        $scope.emailFormat = /^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$/;

    $scope.Reset = {
        Password: "",
        ConfirmPassword: ""
    };

    $scope.ForgotPassword = function () {
        
        $scope.ResetLinkSent = false;
        $scope.Error = false;
        ForgotPasswordService.ForgotPassword($scope.Email)
        .then(function (response) {

            if (response.data == 404) {
                $scope.Error = true;
                $scope.ResetLinkSent = false;
            }
            else {
                $scope.ResetLinkSent = true;
                //$location.path("/CodeSentMessage");
            }


        })
        .catch(function (error) {   // use catch instead of error
            // error
            //$scope.Error = true;
        });

    };
    $scope.CheckCode = function () {
        
        $scope.NotFoundError = false;
        var CodeExists = ForgotPasswordService.CheckCode($scope.VarificationCode)
        CodeExists.then(function (response) {


            
            $scope.products = TransferData.AddUserAndTokenData(response.data.Id, response.data.Token);
            //$state.go('reset_password', { id: response.data.Id,  token: response.data.Token });

            $state.go('reset_password', { id: response.data.Id, token: response.data.Token });




            //$location.path("/reset_password");
        })
         .catch(function (error) {   // use catch instead of error
             // error
             $scope.NotFoundError = true;
             
         });

    }

    $scope.ResetPassword = function () {
        $scope.PasswordError = false;
        $scope.ConfirmPasswordError = false;
        $scope.PasswordNotMatched = false;
        $scope.validateError = false;
        $scope.NullId = false;
        $scope.PasswordResetSuccess = false;

        
        if ($scope.Reset.Password.trim() == '') {
            $scope.PasswordError = true;
            $scope.validateError = true;
        }
        else if ($scope.Reset.ConfirmPassword.trim() == '') {
            $scope.ConfirmPasswordError = true;
            $scope.validateError = true;
        }
        else if ($scope.Reset.Password.trim() != $scope.Reset.ConfirmPassword.trim()) {
            $scope.PasswordNotMatched = true;
            $scope.validateError = true;
        }

        else {

            var userWithToken = TransferData.GetUserWithToken();
            if (userWithToken != undefined && userWithToken.Id != undefined) {
                
                ForgotPasswordService.ResetPassword(userWithToken.Id, userWithToken.Token, $scope.Reset.Password).then(function (response) {
                    $scope.PasswordResetSuccess = true;
                    //$location.path("/PasswordResetSuccess");
                })
             .catch(function (error) {   // use catch instead of error
                 // error
                 $scope.Error = true;
                 $scope.PasswordResetSuccess = false;
             });
            }
            else {
                $scope.validateError = true;
                $scope.NullId = true;
            }


        }

    }

}])
.controller("associateController", ["$scope", "$location", "$timeout", "AuthenticationService", function ($scope, $location, $timeout, AuthenticationService) {
    var controller = this;

    controller.savedSuccessfully = false;
    controller.message = "";
    controller.registerData = {
        userName: AuthenticationService.externalAuthData.userName,
        provider: AuthenticationService.externalAuthData.provider,
        externalAccessToken: AuthenticationService.externalAuthData.externalAccessToken
    };

    controller.registerExternal = function () {
        AuthenticationService.registerExternal($scope.registerData)
         .then(function (response) {
             controller.savedSuccessfully = true;
             controller.message = "User has been registered successfully, you will be redicted to orders page in 2 seconds.";
             controller.startTimer();
         },
       function (response) {
           var errors = [];

           for (var key in response.modelState) {
               errors.push(response.modelState[key]);
           }

           controller.message = "Failed to register user due to:" + errors.join(" ");
       });


    };

    controller.startTimer = function () {
    };
}])
.controller("signupController", ["$scope", "$rootScope", "$timeout", "AuthenticationService", "ngAuthSettings", "$state", function ($scope, $rootScope, $timeout, AuthenticationService, ngAuthSettings, $state) {

    $scope.emailFormat = /^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$/;
    var controller = this;

    controller.submitted = false;
    controller.message = "";
    controller.signupData = {
        Email: "",
        Password: "",
        ConfirmPassword: "",
        TermsAccepted: false,
        NewUser: true
    };
    controller.cancel = $scope.$dismiss;
    controller.errorMessage = "";
    controller.hasError = false;
    controller.busy = false;
    controller.registrationComplete = false;

    controller.login = function () {
        $rootScope.$emit("ShouldLoginEvent");
        $scope.$dismiss();
    };

    $scope.signup = function () {
        
        $scope.EmailError = false;
        $scope.PasswordError = false;
        $scope.ConfirmPasswordError = false;
        $scope.PasswordNotMatched = false;
        $scope.validateError = false;

        
        if (controller.signupData.Email.trim() == '') {
            $scope.Error = false;
            $scope.EmailError = true;
            $scope.validateError = true;
        }
        else if (controller.signupData.Password.trim() == '') {
            $scope.Error = false;
            $scope.PasswordError = true;
            $scope.validateError = true;
        }
        else if (controller.signupData.ConfirmPassword.trim() == '') {
            $scope.Error = false;
            $scope.ConfirmPasswordError = true;
            $scope.validateError = true;
        }
        else if (controller.signupData.Password.trim() != controller.signupData.ConfirmPassword.trim()) {
            $scope.Error = false;
            $scope.LengthError = false;
            $scope.PasswordNotMatched = true;
            $scope.validateError = true;

        }
        else if (controller.signupData.Password.trim().length < 6 || controller.signupData.ConfirmPassword.trim().length < 6) {
            $scope.validateError = true;
            $scope.LengthError = true;
        }


        else {
            
            controller.submitted = false;
            controller.busy = true;
            AuthenticationService.saveRegistration(controller.signupData)
                .then(function (response) {
                    
                    
                    $scope.Error = false;
                    controller.registrationComplete = true;
                },
                 function (response) {
                     

                     var errors = [];

                     for (var key in response.data.modelState) {
                         for (var i = 0; i < response.data.modelState[key].length; i++) {
                             errors.push(response.data.modelState[key][i]);
                         }
                     }

                     //controller.errorMessage = "Failed to register user due to:" + errors.join(" ");
                     //controller.hasError = true;

                     $scope.Error = true;
                 })
                .finally(function () {
                    controller.busy = false;
                });
        }
    };

    controller.facebookSignup = function () {
        var redirectUri = location.protocol + "//" + location.host + "/facebook_authentication_complete.html";
        var externalProviderUrl = ngAuthSettings.apiServiceBaseUri + "api/Account/ExternalLogin?provider=Facebook"
                                                                    + "&response_type=token&client_id=" + ngAuthSettings.clientId
                                                                    + "&redirect_uri=" + redirectUri;
        window.$windowScope = controller;

        var oauthWindow = window.open(externalProviderUrl, "Authenticate Account", "location=0,status=0,width=600,height=750");
    };

    controller.facebookAuthenticationCompletedCallback = function (fragment) {
        $scope.$apply(function () {
            var externalData = {
                provider: fragment.provider,
                externalAccessToken: fragment.external_access_token
            };

            if (fragment.haslocalaccount === "False") {
                AuthenticationService.logOut();

                var registerData = {
                    provider: fragment.provider,
                    userName: fragment.external_user_name,
                    externalAccessToken: fragment.external_access_token
                };

                AuthenticationService.registerExternal(registerData)
                    .then(function (response) {
                        AuthenticationService.obtainAccessToken(externalData)
                        .then(function (response) {
                            controller.signupData.NewUser = false;
                            controller.signupData.Email = response.userName;
                            $scope.$close(controller.signupData);
                        },
                     function (err) {
                         controller.errorMessage = err.error_description;
                         controller.hasError = true;
                     });
                    },
                 function (err) {
                     controller.errorMessage = err.error_description;
                     controller.hasError = true;
                 });
            }
            else {
                AuthenticationService.obtainAccessToken(externalData)
                    .then(function (response) {
                        controller.signupData.NewUser = false;
                        controller.signupData.Email = response.userName;
                        $scope.$close(controller.signupData);
                    },
                 function (err) {
                     controller.errorMessage = err.error_description;
                     controller.hasError = true;
                 });
            }
        });
    };
    //}])
    var controller = this;

    controller.submitted = false;
    controller.message = "";
    controller.signupData = {
        Email: "",
        Password: "",
        ConfirmPassword: "",
        TermsAccepted: false,
        NewUser: true
    };
    controller.cancel = $scope.$dismiss;
    controller.errorMessage = "";
    controller.hasError = false;
    controller.busy = false;
    controller.registrationComplete = false;

    controller.login = function () {
        
        $rootScope.$emit("ShouldLoginEvent");
        $scope.$dismiss();
    };

    controller.signup = function () {
        
        //fsdsdf
        $scope.EmailError = false;
        $scope.PasswordError = false;
        $scope.ConfirmPasswordError = false;
        $scope.PasswordNotMatched = false;
        $scope.validateError = false;
        $scope.LengthError = false;

        if (controller.signupData.Email.trim() == '') {
            $scope.EmailError = true;
            $scope.validateError = true;
        }
        else if (controller.signupData.Password.trim() == '') {
            $scope.PasswordError = true;
            $scope.validateError = true;
        }
        else if (controller.signupData.ConfirmPassword.trim() == '') {
            $scope.ConfirmPasswordError = true;
            $scope.validateError = true;
        }
        else if (controller.signupData.Password.trim() != controller.signupData.ConfirmPassword.trim()) {
            $scope.PasswordNotMatched = true;
            $scope.validateError = true;
        }



        else {

            controller.submitted = false;
            controller.busy = true;
            AuthenticationService.saveRegistration(controller.signupData)
                .then(function (response) {
                    $scope.Error = false;
                    controller.registrationComplete = true;
                },
                 function (response) {
                     var errors = [];

                     for (var key in response.data.modelState) {
                         for (var i = 0; i < response.data.modelState[key].length; i++) {
                             errors.push(response.data.modelState[key][i]);
                         }
                     }

                     //controller.errorMessage = "Failed to register user due to:" + errors.join(" ");
                     //controller.hasError = true;

                     $scope.Error = true;
                 })
                .finally(function () {
                    controller.busy = false;
                });
        }
    };

    controller.facebookSignup = function () {
        var redirectUri = location.protocol + "//" + location.host + "/facebook_authentication_complete.html";
        var externalProviderUrl = ngAuthSettings.apiServiceBaseUri + "api/Account/ExternalLogin?provider=Facebook"
                                                                    + "&response_type=token&client_id=" + ngAuthSettings.clientId
                                                                    + "&redirect_uri=" + redirectUri;
        window.$windowScope = controller;

        var oauthWindow = window.open(externalProviderUrl, "Authenticate Account", "location=0,status=0,width=600,height=750");
    };

    controller.facebookAuthenticationCompletedCallback = function (fragment) {
        $scope.$apply(function () {
            var externalData = {
                provider: fragment.provider,
                externalAccessToken: fragment.external_access_token
            };

            if (fragment.haslocalaccount === "False") {
                AuthenticationService.logOut();

                var registerData = {
                    provider: fragment.provider,
                    userName: fragment.external_user_name,
                    externalAccessToken: fragment.external_access_token
                };

                AuthenticationService.registerExternal(registerData)
                    .then(function (response) {
                        AuthenticationService.obtainAccessToken(externalData)
                        .then(function (response) {
                            controller.signupData.NewUser = false;
                            controller.signupData.Email = response.userName;
                            $scope.$close(controller.signupData);
                        },
                     function (err) {
                         controller.errorMessage = err.error_description;
                         controller.hasError = true;
                     });
                    },
                 function (err) {
                     controller.errorMessage = err.error_description;
                     controller.hasError = true;
                 });
            }
            else {
                AuthenticationService.obtainAccessToken(externalData)
                    .then(function (response) {
                        controller.signupData.NewUser = false;
                        controller.signupData.Email = response.userName;
                        $scope.$close(controller.signupData);
                    },
                 function (err) {
                     controller.errorMessage = err.error_description;
                     controller.hasError = true;
                 });
            }
        });
    };
}])
.controller("loggedInHeaderController", ["$scope", "$rootScope", "Azureservice", "localStorageService", function ($scope, $rootScope, Azureservice, localStorageService) {
    var controller = this;

    controller.addActionSubmenuVisible = false;
    controller.searchInputVisible = false;
    controller.products = [];
    controller.individualFlag = false;

    controller.init = function () {
        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("GetMyPersonalProfile", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
            .then(function (user) {
                
                controller.individualFlag = user.IsIndividual;
                console.log(controller.individualFlag);
            });
        
        Azureservice.invokeApi("GetMyCompanyProducts", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (products) {
              controller.products = products;
              $scope.$broadcast("dataready", null);
          });
    };

   
    controller.hideAddActionSubmenu = function () {
        controller.addActionSubmenuVisible = false;
    }

    controller.showAddActionSubmenu = function () {
        controller.addActionSubmenuVisible = true;
    }

    controller.hideSearchInput = function () {
        controller.searchInputVisible = false;
    }

    controller.showSearchInput = function () {
        controller.searchInputVisible = true;
    }

    $scope.search = function () {
        
        var authData = localStorageService.get("authorizationData");
        //return Azureservice.invokeApi("SearchCompaniesAndProducts?query=" + encodeURIComponent(searchQuery) + "&maxRecords=10", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } });
    };

    controller.init();
}])
.controller("loggedInCompanyAndProductHeaderController", ["$scope", "$stateParams", "Azureservice", "localStorageService", function ($scope, $stateParams, Azureservice, localStorageService) {
    var controller = this;

    controller.companyId = $stateParams.companyId;
    controller.productId = $stateParams.productId;
    controller.companyUri = $stateParams.companyUri;
    controller.companyAndProductNavbarVisible = true;
    controller.addActionSubmenuVisible = false;
    controller.myProducts = [];
    controller.companyProducts = [];
    controller.companyConnections = [];
    controller.isConnected = true;

    controller.init = function () {
        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("GetMyCompanyConnections", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (companyConnections) {
              controller.companyConnections = companyConnections;

              var isConnected = false;

              for (var i = 0, len = companyConnections.length; i < len; i++) {
                  if (companyConnections[i].CompanyID == controller.companyId) {
                      isConnected = true;

                      break;
                  }
              }

              controller.isConnected = isConnected;
          });

        Azureservice.invokeApi("GetMyCompanyProducts", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (products) {
              controller.myProducts = products;
          });

        if (controller.companyId) {
            Azureservice.invokeApi("GetCompanyProductSummaries?companyId=" + controller.companyId, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
              .then(function (products) {
                  controller.companyProducts = products;
              });
        }
        else if (controller.companyUri) {
            Azureservice.invokeApi("GetCompanyProductSummariesByCompanyUri?companyUri=" + controller.companyUri, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
              .then(function (products) {
                  controller.companyProducts = products;
              });
        }
    };

    controller.hideAddActionSubmenu = function () {
        controller.addActionSubmenuVisible = false;
    };

    controller.showAddActionSubmenu = function () {
        controller.addActionSubmenuVisible = true;
    };

    controller.hideCompanyAndProductNavbar = function () {
        controller.companyAndProductNavbarVisible = false;
    };

    controller.showCompanyAndProductNavbar = function () {
        controller.companyAndProductNavbarVisible = true;
    };

    controller.addConnection = function () {
        if (controller.isConnected) {
            return;
        }

        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("AddCompanyConnection?companyId=" + controller.companyId, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function () {
              controller.isConnected = true;
          });
    };

    controller.init();
}])
.controller("footerController", ["$scope", "$rootScope", function ($scope, $rootScope) {
    var controller = this;

    controller.showFooter = false;

    controller.login = function () {
        
        $rootScope.$emit("ShouldLoginEvent");
    };

    controller.signup = function () {
        $rootScope.$emit("ShouldSignupEvent");
    };

    $rootScope.$on("$stateChangeSuccess", function (event, toState) {
        if (toState.name === "search") {
            controller.showFooter = false;
        }
        else {
            controller.showFooter = true;
        }
    });
}])
.controller("searchController", ["$scope", "$stateParams", "Azureservice", "localStorageService", function ($scope, $stateParams, Azureservice, localStorageService) {
    var controller = this;

    

    controller.searchQuery = $stateParams.query;
    controller.companiesVisible = ($stateParams.show === "companies");
    controller.products = [];
    controller.companies = [];

    controller.init = function () {
        if (!controller.searchQuery || controller.searchQuery.length < 3) {
            return;
        }

        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("SearchCompanies?query=" + controller.searchQuery + "&maxRecords=20", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (companies) {
              controller.companies = companies;
          });

        Azureservice.invokeApi("SearchProducts?query=" + controller.searchQuery + "&maxRecords=20", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (products) {
              controller.products = products;
          });
    };

    controller.showProducts = function () {
        controller.companiesVisible = false;
    };

    controller.showCompanies = function () {
        controller.companiesVisible = true;
    };

    controller.init();
}])
.controller("personalProfileDetailsController", ["$scope", "Azureservice", "localStorageService", "$timeout", "$rootScope", "Countries", function ($scope, Azureservice, localStorageService, $timeout, $rootScope, Countries) {
    var controller = this;

    controller.user = {};
    controller.validationErrors = [];
    controller.flowOptions = {
        target: "/api/UserProfileImageUpload",
        permanentErrors: [500, 501],
        maxChunkRetries: 1,
        chunkRetryInterval: 2000,
        simultaneousUploads: 4,
        progressCallbacksInterval: 500,
        headers: function () {
            var authData = localStorageService.get("authorizationData");

            return { "Authorization": "Bearer " + authData.token };
        },
        generateUniqueIdentifier: function () {
            return (new Date()).getTime();
        }
    };
    controller.hasImagesToUpload = false;
    controller.ImageType = null;
    controller.busy = false;
    controller.countries = Countries;
    controller.errorMessage = null;
    controller.hasError = false;
    controller.confirmationMessage = null;
    controller.hasConfirmation = false;

    controller.init = function () {
        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("GetMyPersonalProfile", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (user) {
              controller.user = user;
              $scope.Individual = user.IsIndividual;
              
              $scope.$broadcast("dataready", null);
          });
    };

    controller.updateMyPersonalProfile = function ($flow) {
        
        var authData = localStorageService.get("authorizationData");

        controller.resetNotifications();
        controller.busy = true;
        Azureservice.invokeApi("UpdateMyPersonalProfile", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: controller.user })
            .then(function (user) {
                controller.user = user;
                controller.showConfirmation("Your personal details were saved");

                if (controller.hasImagesToUpload !== true) {
                    $timeout(function () {
                        $rootScope.$emit("UserProfileImageChanged");
                    }, 1000);
                }

                controller.hasImagesToUpload = false;
            })
            .finally(function () {
                controller.busy = false;
            });
    };



    $rootScope.$on("CallParentMethod", function ($flow) {
        
        controller.updateMyPersonalProfile($flow);
    });



    controller.getUserProfileImage = function () {
            
        if (!controller.user.Image) {
            return "/img/user_image.svg";
        }

        return "data:image/JPEG;base64," + controller.user.Image;
    };

    controller.flowFilesSubmitted = function ($files, $event, $flow) {
        if (!$event) {
            return;
        }

        var i;
        var reader = new FileReader();
        var base64DataBeginMarker = ";base64,";
        var base64DataBeginIndex;

        reader.addEventListener("load", function () {
            base64DataBeginIndex = reader.result.indexOf(base64DataBeginMarker);
            controller.user.Image = reader.result.substring(base64DataBeginIndex + base64DataBeginMarker.length);
            controller.ImageType = $files[0].getType();
            $flow.cancel();
            controller.hasImagesToUpload = true;
            $scope.$apply();
        }, false);
        reader.readAsDataURL($files[0].file);
    };

    controller.base64StringToBlob = function (base64String, contentType, sliceSize) {
        contentType = contentType || "";
        sliceSize = sliceSize || 512;

        var byteCharacters = atob(base64String);
        var byteArrays = [];
        var slice;
        var byteNumbers;
        var byteArray;
        var blob;

        for (var offset = 0; offset < byteCharacters.length; offset += sliceSize) {
            slice = byteCharacters.slice(offset, offset + sliceSize);
            byteNumbers = new Array(slice.length);

            for (var i = 0; i < slice.length; i++) {
                byteNumbers[i] = slice.charCodeAt(i);
            }

            byteArray = new Uint8Array(byteNumbers);
            byteArrays.push(byteArray);
        }

        blob = new Blob(byteArrays, { type: contentType });

        return blob;
    };

    controller.setUserCountry = function (country) {
        controller.user.Country = country;
    }

    controller.showError = function (errorMessage) {
        controller.errorMessage = errorMessage;
        controller.hasError = true;

        $timeout(function () {
            controller.errorMessage = null;
            controller.hasError = false;
        }, 15000);
        $("html, body").animate({
            scrollTop: 0
        }, 500);
    };

    controller.showConfirmation = function (confirmationMessage) {
        controller.confirmationMessage = confirmationMessage;
        controller.hasConfirmation = true;

        $timeout(function () {
            controller.confirmationMessage = null;
            controller.hasConfirmation = false;
        }, 15000);
        $("html, body").animate({
            scrollTop: 0
        }, 500);
    };

    controller.resetNotifications = function () {
        controller.errorMessage = null;
        controller.hasError = false;
        controller.confirmationMessage = null;
        controller.hasConfirmation = false;
    };

    controller.init();
}])
.controller("personalProfileNotificationsController", ["$scope", "Azureservice", "localStorageService", "$timeout", "$rootScope", function ($scope, Azureservice, localStorageService, $timeout, $rootScope) {
    var controller = this;

    controller.user = {};
    controller.validationErrors = [];
    controller.busy = false;
    controller.errorMessage = null;
    controller.hasError = false;
    controller.confirmationMessage = null;
    controller.hasConfirmation = false;

    controller.init = function () {
        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("GetMyEmailNotificationFrequencies", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (user) {
              controller.user = user;
          });
    };

    controller.updateMyEmailNotificationFrequencies = function () {
        var authData = localStorageService.get("authorizationData");

        controller.resetNotifications();
        controller.busy = true;
        Azureservice.invokeApi("UpdateMyEmailNotificationFrequencies", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: controller.user })
            .then(function (user) {
                controller.user = user;
                controller.showConfirmation("Your notification settings were saved");
            })
            .finally(function () {
                controller.busy = false;
            });
    };
    controller.showError = function (errorMessage) {
        controller.errorMessage = errorMessage;
        controller.hasError = true;

        $timeout(function () {
            controller.errorMessage = null;
            controller.hasError = false;
        }, 15000);
        $("html, body").animate({
            scrollTop: 0
        }, 500);
    };

    controller.showConfirmation = function (confirmationMessage) {
        controller.confirmationMessage = confirmationMessage;
        controller.hasConfirmation = true;

        $timeout(function () {
            controller.confirmationMessage = null;
            controller.hasConfirmation = false;
        }, 15000);
        $("html, body").animate({
            scrollTop: 0
        }, 500);
    };

    controller.resetNotifications = function () {
        controller.errorMessage = null;
        controller.hasError = false;
        controller.confirmationMessage = null;
        controller.hasConfirmation = false;
    };

    controller.init();
}])
.controller("personalProfileSocialMediaController", ["$scope", "Azureservice", "localStorageService", "$timeout", "$rootScope", function ($scope, Azureservice, localStorageService, $timeout, $rootScope) {
    var controller = this;

    controller.user = {};
    controller.validationErrors = [];
    controller.busy = false;
    controller.errorMessage = null;
    controller.hasError = false;
    controller.confirmationMessage = null;
    controller.hasConfirmation = false;

    controller.init = function () {
        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("GetMySocialMediaLinks", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (user) {
              controller.user = user;
          });
    };

    controller.updateMySocialMediaLinks = function () {
        var authData = localStorageService.get("authorizationData");

        controller.resetNotifications();
        controller.busy = true;
        Azureservice.invokeApi("UpdateMySocialMediaLinks", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: controller.user })
            .then(function (user) {
                controller.user = user;
                controller.showConfirmation("Your social media links were saved");
            })
            .finally(function () {
                controller.busy = false;
            });
    };
    controller.showError = function (errorMessage) {
        controller.errorMessage = errorMessage;
        controller.hasError = true;

        $timeout(function () {
            controller.errorMessage = null;
            controller.hasError = false;
        }, 15000);
        $("html, body").animate({
            scrollTop: 0
        }, 500);
    };

    controller.showConfirmation = function (confirmationMessage) {
        controller.confirmationMessage = confirmationMessage;
        controller.hasConfirmation = true;

        $timeout(function () {
            controller.confirmationMessage = null;
            controller.hasConfirmation = false;
        }, 15000);
        $("html, body").animate({
            scrollTop: 0
        }, 500);
    };

    controller.resetNotifications = function () {
        controller.errorMessage = null;
        controller.hasError = false;
        controller.confirmationMessage = null;
        controller.hasConfirmation = false;
    };

    controller.init();
}])
.controller("companyProfileDetailsController", ["$scope", "Azureservice", "localStorageService", "$timeout", "$rootScope", "TransferData", "Countries", function ($scope, Azureservice, localStorageService, $timeout, $rootScope,TransferData, Countries, CompanySectors) {
    var controller = this;
    $scope.company = {};
    controller.flowOptions = {
        target: "/api/CompanyProfileImageUpload",
        permanentErrors: [500, 501],
        maxChunkRetries: 1,
        chunkRetryInterval: 2000,
        simultaneousUploads: 4,
        progressCallbacksInterval: 500,
        headers: function () {
            var authData = localStorageService.get("authorizationData");

            return { "Authorization": "Bearer " + authData.token };
        },
        generateUniqueIdentifier: function () {
            return (new Date()).getTime();
        }
    };
    controller.hasImagesToUpload = false;
    controller.ImageType = null;
    controller.companySectors = CompanySectors;
    controller.validationErrors = [];
    controller.busy = false;
    controller.errorMessage = null;
    controller.hasError = false;
    controller.confirmationMessage = null;
    controller.hasConfirmation = false;
    $scope.LogoNotUploaded = false;
    $scope.dataHasLoaded = false;
    $scope.tags = [];
    
    //$scope.countries = Countries;
    $scope.SelectedCountries =[];

    controller.init = function () {
        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("GetMyCompanyProfile", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (company) {
              debugger
              controller.company = company;
              $scope.SelectedCountries = company.Countries;
              $scope.tags = controller.company.Skills;
              
          });
        Azureservice.invokeApi("GetSectors", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
            .then(function (sectors) {
                controller.companySectors = sectors;
            });

        Azureservice.invokeApi("GetCountries", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
            .then(function (CountryList) {
                
                $scope.countries = CountryList;
            });

        Azureservice.invokeApi("GetSkills", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
           .then(function (skills) {
               debugger
               $scope.skillList = skills;
               //$rootScope.CoreServices = skills;
               $scope.dataHasLoaded = true;
               TransferData.AddSkillsData($scope.skillList);
           });


    };

    controller.setCompanySector = function (sector) {
        
        controller.company.Sector = sector;
    };

    controller.updateCompanyUri = function () {
        if (!controller.company.DisplayName) {
            controller.company.URI = "";
        }

        var companyUri = controller.company.DisplayName.replace(/ /g, "-");

        controller.company.URI = companyUri.replace(/[^\w-_]+/g, "").toLowerCase();
    };

    controller.updateMyCompanyProfile = function ($flow) {
        var authData = localStorageService.get("authorizationData");
        
        controller.resetNotifications();
        //controller.busy = true;
        ;
        controller.company.Countries = $scope.SelectedCountries;

        if (controller.company.URI) {
            var companyUri = controller.company.DisplayName.replace(/ /g, "-");

            controller.company.URI = companyUri.replace(/[^\w-_]+/g, "").toLowerCase();
        }
        if (!controller.company.Image )
        {
            if ($scope.InValidProfileImage == true) {
                return
            }
            
            return $scope.LogoNotUploaded = true;

             
        }
        

        Azureservice.invokeApi("UpdateMyCompanyProfile", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: controller.company })
            .then(function (company) {
                controller.company = company;
                controller.showConfirmation("Your company profile was saved");
                $scope.InValidProfileImage = false;
                $scope.LogoNotUploaded = false;
                $scope.InvalidHeader = false;

                if (controller.hasImagesToUpload !== true) {
                    $timeout(function () {
                        $rootScope.$emit("CompanyProfileImageChanged");
                    }, 1000);
                }

                controller.hasImagesToUpload = false;
            })
            .finally(function () {
                controller.busy = false;
            });
    };

    controller.getCompanyProfileImage = function () {
        
        if (!controller.company.Image) {
            return "/img/logo-home-full.png";
            
        }

        return "data:image/JPEG;base64," + controller.company.Image;
    };

    controller.flowFilesSubmitted = function ($files, $event, $flow) {
        if (!$event) {
            return;
        }

        var i;
        var reader = new FileReader();
        var base64DataBeginMarker = ";base64,";
        var base64DataBeginIndex;
        $scope.InValidProfileImage = false;
        $scope.LogoNotUploaded = false;

        var ext = $files[0].name.match(/\.(.+)$/)[1];

        if (angular.lowercase(ext) === 'jpg' || angular.lowercase(ext) === 'jpeg' || angular.lowercase(ext) === 'png') {

            reader.addEventListener("load", function () {
                base64DataBeginIndex = reader.result.indexOf(base64DataBeginMarker);
                controller.company.Image = reader.result.substring(base64DataBeginIndex + base64DataBeginMarker.length);
                controller.ImageType = $files[0].getType();
                $flow.cancel();
                controller.hasImagesToUpload = true;
                $scope.$apply();
            }, false);
            reader.readAsDataURL($files[0].file);

            $scope.InValidProfileImage = false;
            
        }
        else {
            
            $scope.InValidProfileImage = true;

        }
    };
    controller.HeaderImageFile = function ($files, $event, $flow) {
        if (!$event) {
            return;
        }
        
        var i;
        var reader = new FileReader();
        var base64DataBeginMarker = ";base64,";
        var base64DataBeginIndex;
        $scope.InvalidHeader = false;

        var ext = $files[0].name.match(/\.(.+)$/)[1];

        if (angular.lowercase(ext) === 'jpg' || angular.lowercase(ext) === 'jpeg' || angular.lowercase(ext) === 'png') {

            reader.addEventListener("load", function () {
                base64DataBeginIndex = reader.result.indexOf(base64DataBeginMarker);
                controller.company.HeaderImage = reader.result.substring(base64DataBeginIndex + base64DataBeginMarker.length);
                controller.ImageType = $files[0].getType();
                $flow.cancel();
                controller.hasImagesToUpload = true;
                $scope.$apply();
            }, false);
            reader.readAsDataURL($files[0].file);

            $scope.InvalidHeader = false;
        }
        else {
            
            $scope.InvalidHeader = true;
        }

       

        
    };

    controller.base64StringToBlob = function (base64String, contentType, sliceSize) {
        contentType = contentType || "";
        sliceSize = sliceSize || 512;

        var byteCharacters = atob(base64String);
        var byteArrays = [];
        var slice;
        var byteNumbers;
        var byteArray;
        var blob;

        for (var offset = 0; offset < byteCharacters.length; offset += sliceSize) {
            slice = byteCharacters.slice(offset, offset + sliceSize);
            byteNumbers = new Array(slice.length);

            for (var i = 0; i < slice.length; i++) {
                byteNumbers[i] = slice.charCodeAt(i);
            }

            byteArray = new Uint8Array(byteNumbers);
            byteArrays.push(byteArray);
        }

        blob = new Blob(byteArrays, { type: contentType });

        return blob;
    };
    controller.showError = function (errorMessage) {
        controller.errorMessage = errorMessage;
        controller.hasError = true;

        $timeout(function () {
            controller.errorMessage = null;
            controller.hasError = false;
        }, 15000);
        $("html, body").animate({
            scrollTop: 0
        }, 500);
    };

    controller.showConfirmation = function (confirmationMessage) {
        controller.confirmationMessage = confirmationMessage;
        controller.hasConfirmation = true;

        $timeout(function () {
            controller.confirmationMessage = null;
            controller.hasConfirmation = false;
        }, 15000);
        $("html, body").animate({
            scrollTop: 0
        }, 500);
    };

    controller.resetNotifications = function () {
        controller.errorMessage = null;
        controller.hasError = false;
        controller.confirmationMessage = null;
        controller.hasConfirmation = false;
    };

    $scope.setCompanyCountry = function (country) {
        var index = $scope.SelectedCountries.indexOf(country);
        
        if (index < 0) {
            
            $scope.SelectedCountries.push(country);
        }
    }

    $scope.deleteCompanyCountry = function (country) {
        var index = $scope.SelectedCountries.indexOf(country);
        $scope.SelectedCountries.splice( index,1);
    }

    $scope.getCompanyHeaderImage = function () {
        
        if (!controller.company.HeaderImage) {
            return "/img/user_image.svg";
        }

        return "data:image/JPEG;base64," + controller.company.HeaderImage;
    };

    controller.init();
}])
.controller("companyProfileTeamController", ["$scope", "Azureservice", "localStorageService", "$timeout", "$rootScope", function ($scope, Azureservice, localStorageService, $timeout, $rootScope) {
    var controller = this;
    
    controller.companyMembers = [];
    controller.validationErrors = [];
    controller.busy = false;
    controller.userSearchQuery = null;
    controller.usersFromSearch = [];
    controller.errorMessage = null;
    controller.hasError = false;
    controller.confirmationMessage = null;
    controller.hasConfirmation = false;
    controller.noUsersFound = false;
    $scope.userLevels = [];

    controller.init = function () {
        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("GetMyCompanyMembers", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (companyMembers) {
              controller.companyMembers = companyMembers;
              $scope.$broadcast("dataready", null);
            });

        Azureservice.invokeApi("GetUseLevels", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
            .then(function (levels) {
                $scope.userLevels = levels;
                $scope.$broadcast("dataready", null);
            });

    };

    controller.searchUsersForCompanyTeamMembers = function () {
        if (!controller.userSearchQuery || controller.userSearchQuery.length < 2) {
            return;
        }

        var authData = localStorageService.get("authorizationData");

        controller.noUsersFound = false;
        Azureservice.invokeApi("SearchUsersForCompanyTeamMembers?query=" + encodeURIComponent(controller.userSearchQuery), { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (usersFromSearch) {
              controller.usersFromSearch = usersFromSearch;

              if (usersFromSearch.length === 0) {
                  controller.noUsersFound = true;
              }
          });
    };
    
    controller.addUserToTeam = function (user) {
        var newCompanyTeamMemberDto = { UserName: user.UserName };
        var authData = localStorageService.get("authorizationData");

        controller.resetNotifications();
        controller.busy = true;
        Azureservice.invokeApi("AddNewMemberToMyCompany", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: newCompanyTeamMemberDto })
            .then(function (companyMember) {
                for (var i = 0, len = controller.usersFromSearch.length; i < len; i++) {
                    if (controller.usersFromSearch[i].UserName === user.UserName) {
                        controller.usersFromSearch.splice(i, 1);

                        break;
                    }
                }

                Azureservice.invokeApi("GetMyCompanyMembers", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
                 .then(function (companyMembers) {
                     controller.companyMembers = companyMembers;
                     controller.showConfirmation("Team member added");
                 });
            })
            .finally(function () {
                controller.busy = false;
            });
    };

    controller.deleteCompanyMember = function (companyMember) {
        var authData = localStorageService.get("authorizationData");

        controller.resetNotifications();
        controller.busy = true;
        Azureservice.invokeApi("RemoveMemberFromMyCompany", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: companyMember })
            .then(function (companyMember) {
                Azureservice.invokeApi("GetMyCompanyMembers", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
                 .then(function (companyMembers) {
                     controller.companyMembers = companyMembers;
                     controller.showConfirmation("Team member removed");
                 });
            })
            .finally(function () {
                controller.busy = false;
            });
    };

    controller.editCompanyMember = function (companyMember) {
        companyMember.Editing = true;
        companyMember.userLevels = $scope.userLevels;
       
    };


    controller.updateCompanyMember = function (companyMember) {
        var authData = localStorageService.get("authorizationData");

        companyMember.Editing = false;
        controller.resetNotifications();
        controller.busy = true;
        Azureservice.invokeApi("UpdateMemberOfMyCompany", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: companyMember })
            .then(function (companyMember) {
                controller.showConfirmation("Team member details saved");
            })
            .finally(function () {
                controller.busy = false;
            });
    };

    controller.getUserDisplayName = function (user) {
        if (user.FirstName && user.LastName) {
            return user.FirstName + " " + user.LastName;
        }

        if (user.FirstName) {
            FirstName
        }

        if (user.LastName) {
            return user.LastName;
        }

        return user.UserName;
    };

    controller.getCompanyMemberUserDisplayName = function (companyMember) {
        if (companyMember.UserFirstName && companyMember.UserLastName) {
            return companyMember.UserFirstName + " " + companyMember.UserLastName;
        }

        if (companyMember.UserFirstName) {
            return companyMember.UserFirstName;
        }

        if (companyMember.UserLastName) {
            return companyMember.UserLastName;
        }

        return companyMember.UserUserName;
    };

    controller.showError = function (errorMessage) {
        controller.errorMessage = errorMessage;
        controller.hasError = true;

        $timeout(function () {
            controller.errorMessage = null;
            controller.hasError = false;
        }, 15000);
        $("html, body").animate({
            scrollTop: 0
        }, 500);
    };

    controller.showConfirmation = function (confirmationMessage) {
        controller.confirmationMessage = confirmationMessage;
        controller.hasConfirmation = true;

        $timeout(function () {
            controller.confirmationMessage = null;
            controller.hasConfirmation = false;
        }, 15000);
        $("html, body").animate({
            scrollTop: 0
        }, 500);
    };

    controller.resetNotifications = function () {
        controller.errorMessage = null;
        controller.hasError = false;
        controller.confirmationMessage = null;
        controller.hasConfirmation = false;
    };

    controller.init();
}])

.controller("companyProfileDetailsNews", ["$scope", "Azureservice", "localStorageService", "$timeout", "$rootScope", function ($scope, Azureservice, localStorageService, $timeout, $rootScope) {
        var controller = this;

        
        controller.init = function () {
            var authData = localStorageService.get("authorizationData");

            //Azureservice.invokeApi("GetMyCompanyMembers", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
            //  .then(function (companyMembers) {
            //      controller.companyMembers = companyMembers;
            //      $scope.$broadcast("dataready", null);
            //  });
        };

        controller.init();
    }])
.controller("companyProfileFollowerGroupsController", ["$scope", "Azureservice", "localStorageService", "$timeout", "$rootScope", function ($scope, Azureservice, localStorageService, $timeout, $rootScope) {
    var controller = this;

    controller.followerGroups = [];
    controller.validationErrors = [];
    controller.busy = false;
    controller.userSearchQuery = null;
    controller.usersFromSearch = [];
    controller.errorMessage = null;
    controller.hasError = false;
    controller.confirmationMessage = null;
    controller.hasConfirmation = false;
    controller.noUsersFound = false;
    controller.newFollowerGroupPopup = false;
    controller.newCompanyFollowerGroupName = null;

    controller.init = function () {
        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("GetMyCompanyFollowerGroupSummaries", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (followerGroups) {
              controller.followerGroups = followerGroups;

              for (var i = 0, len = controller.followerGroups.length; i < len; i++) {
                  controller.followerGroups[i].Editing = false;
              }
          });
    };

    controller.showNewFollowerGroupPopup = function () {
        controller.newFollowerGroupPopup = true;
    };

    controller.searchUsersForCompanyTeamMembers = function () {
        if (!controller.userSearchQuery || controller.userSearchQuery.length < 2) {
            return;
        }

        var authData = localStorageService.get("authorizationData");

        controller.noUsersFound = false;
        Azureservice.invokeApi("SearchUsersForCompanyTeamMembers?query=" + encodeURIComponent(controller.userSearchQuery), { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (usersFromSearch) {
              controller.usersFromSearch = usersFromSearch;

              if (usersFromSearch.length === 0) {
                  controller.noUsersFound = true;
              }
          });
    };

    controller.addNewFollowerGroup = function () {
        if (!controller.newCompanyFollowerGroupName || controller.newCompanyFollowerGroupName.length === 0) {
            return;
        }

        var newCompanyFollowerGroupDto = { Name: controller.newCompanyFollowerGroupName };
        var authData = localStorageService.get("authorizationData");

        controller.resetNotifications();
        controller.busy = true;
        Azureservice.invokeApi("NewCompanyFollowerGroup", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: newCompanyFollowerGroupDto })
            .then(function (followerGroup) {
                controller.followerGroups.push(followerGroup);
                controller.newFollowerGroupPopup = false;
                controller.newCompanyFollowerGroupName = null;

                Azureservice.invokeApi("GetMyCompanyFollowerGroupSummaries", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
                  .then(function (followerGroups) {
                      controller.followerGroups = followerGroups;

                      for (var i = 0, len = controller.followerGroups.length; i < len; i++) {
                          controller.followerGroups[i].Editing = false;
                      }

                      controller.showConfirmation("New group added");
                  });
            })
            .finally(function () {
                controller.busy = false;
            });
    };

    controller.deleteFollowerGroup = function (followerGroup) {
        var authData = localStorageService.get("authorizationData");

        controller.resetNotifications();
        controller.busy = true;
        Azureservice.invokeApi("DeleteCompanyFollowerGroup", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: followerGroup })
            .then(function (followerGroup) {
                Azureservice.invokeApi("GetMyCompanyFollowerGroupSummaries", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
                 .then(function (followerGroups) {
                     controller.followerGroups = followerGroups;

                     for (var i = 0, len = controller.followerGroups.length; i < len; i++) {
                         controller.followerGroups[i].Editing = false;
                     }

                     controller.showConfirmation("Group deleted");
                 });
            })
            .finally(function () {
                controller.busy = false;
            });
    };

    controller.editFollowerGroup = function (group) {
        group.Editing = true;
    };

    controller.updateFollowerGroup = function (followerGroup) {
        var authData = localStorageService.get("authorizationData");

        followerGroup.Editing = false;
        controller.resetNotifications();
        controller.busy = true;
        Azureservice.invokeApi("UpdateCompanyFollowerGroup", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: followerGroup })
            .then(function (followerGroup) {
                controller.showConfirmation("Group updated");
            })
            .finally(function () {
                controller.busy = false;
            });
    };

    controller.getUserDisplayName = function (user) {
        if (user.FirstName && user.LastName) {
            return user.FirstName + " " + user.LastName;
        }

        if (user.FirstName) {
            FirstName
        }

        if (user.LastName) {
            return user.LastName;
        }

        return user.UserName;
    };

    controller.getCompanyMemberUserDisplayName = function (companyMember) {
        if (companyMember.UserFirstName && companyMember.UserLastName) {
            return companyMember.UserFirstName + " " + companyMember.UserLastName;
        }

        if (companyMember.UserFirstName) {
            return companyMember.UserFirstName;
        }

        if (companyMember.UserLastName) {
            return companyMember.UserLastName;
        }

        return companyMember.UserUserName;
    };

    controller.showError = function (errorMessage) {
        controller.errorMessage = errorMessage;
        controller.hasError = true;

        $timeout(function () {
            controller.errorMessage = null;
            controller.hasError = false;
        }, 15000);
        $("html, body").animate({
            scrollTop: 0
        }, 500);
    };

    controller.showConfirmation = function (confirmationMessage) {
        controller.confirmationMessage = confirmationMessage;
        controller.hasConfirmation = true;

        $timeout(function () {
            controller.confirmationMessage = null;
            controller.hasConfirmation = false;
        }, 15000);
        $("html, body").animate({
            scrollTop: 0
        }, 500);
    };

    controller.resetNotifications = function () {
        controller.errorMessage = null;
        controller.hasError = false;
        controller.confirmationMessage = null;
        controller.hasConfirmation = false;
    };

    controller.init();
}])
.controller("companyProfileNotificationsController", ["$scope", "Azureservice", "localStorageService", "$timeout", "$rootScope", function ($scope, Azureservice, localStorageService, $timeout, $rootScope) {
    var controller = this;

    controller.company = {};
    controller.validationErrors = [];
    controller.busy = false;
    controller.errorMessage = null;
    controller.hasError = false;
    controller.confirmationMessage = null;
    controller.hasConfirmation = false;

    controller.init = function () {
        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("GetMyCompanyEmailNotificationFrequencies", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (company) {
              controller.company = company;
          });
    };

    controller.updateMyCompanyEmailNotificationFrequencies = function () {
        var authData = localStorageService.get("authorizationData");

        controller.resetNotifications();
        controller.busy = true;
        Azureservice.invokeApi("UpdateMyCompanyEmailNotificationFrequencies", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: controller.company })
            .then(function (company) {
                controller.company = company;
                controller.showConfirmation("Your notification settings were saved");
            })
            .finally(function () {
                controller.busy = false;
            });
    };

    controller.showError = function (errorMessage) {
        controller.errorMessage = errorMessage;
        controller.hasError = true;

        $timeout(function () {
            controller.errorMessage = null;
            controller.hasError = false;
        }, 15000);
        $("html, body").animate({
            scrollTop: 0
        }, 500);
    };

    controller.showConfirmation = function (confirmationMessage) {
        controller.confirmationMessage = confirmationMessage;
        controller.hasConfirmation = true;

        $timeout(function () {
            controller.confirmationMessage = null;
            controller.hasConfirmation = false;
        }, 15000);
        $("html, body").animate({
            scrollTop: 0
        }, 500);
    };

    controller.resetNotifications = function () {
        controller.errorMessage = null;
        controller.hasError = false;
        controller.confirmationMessage = null;
        controller.hasConfirmation = false;
    };

    controller.init();
}])
.controller("companyProfileSocialMediaController", ["$scope", "Azureservice", "localStorageService", "$timeout", "$rootScope", function ($scope, Azureservice, localStorageService, $timeout, $rootScope) {
    var controller = this;

    controller.company = {};
    controller.validationErrors = [];
    controller.busy = false;
    controller.errorMessage = null;
    controller.hasError = false;
    controller.confirmationMessage = null;
    controller.hasConfirmation = false;

    controller.init = function () {
        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("GetMyCompanySocialMediaLinks", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (company) {
              controller.company = company;
          });
    };

    controller.updateMyCompanySocialMediaLinks = function () {
        var authData = localStorageService.get("authorizationData");

        controller.resetNotifications();
        controller.busy = true;
        Azureservice.invokeApi("UpdateMyCompanySocialMediaLinks", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: controller.company })
            .then(function (company) {
                controller.company = company;
                controller.showConfirmation("Your company's social media links were saved");
            })
            .finally(function () {
                controller.busy = false;
            });
    };

    controller.showError = function (errorMessage) {
        controller.errorMessage = errorMessage;
        controller.hasError = true;

        $timeout(function () {
            controller.errorMessage = null;
            controller.hasError = false;
        }, 15000);
        $("html, body").animate({
            scrollTop: 0
        }, 500);
    };

    controller.showConfirmation = function (confirmationMessage) {
        controller.confirmationMessage = confirmationMessage;
        controller.hasConfirmation = true;

        $timeout(function () {
            controller.confirmationMessage = null;
            controller.hasConfirmation = false;
        }, 15000);
        $("html, body").animate({
            scrollTop: 0
        }, 500);
    };

    controller.resetNotifications = function () {
        controller.errorMessage = null;
        controller.hasError = false;
        controller.confirmationMessage = null;
        controller.hasConfirmation = false;
    };

    controller.init();
}])
.controller("companyConnectionsController", ["$scope", "Azureservice", "localStorageService", function ($scope, Azureservice, localStorageService) {
    var controller = this;

    controller.companyConnections = [];
    controller.companyConnectionSearchString = null;

    controller.init = function () {
        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("GetMyCompanyConnections", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (companyConnections) {
              controller.companyConnections = companyConnections;
          });
    };

    controller.init();
}])
.controller("personalConnectionsController", ["$scope", "Azureservice", "localStorageService", "$uibModal", "$timeout", function ($scope, Azureservice, localStorageService, $uibModal, $timeout) {
    var controller = this;

    controller.followers = [];
    controller.followerGroups = [];
    controller.personalConnectionSearchString = null;
    controller.busy = false;
    controller.errorMessage = null;
    controller.hasError = false;
    controller.confirmationMessage = null;
    controller.hasConfirmation = false;

    controller.init = function () {
        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("GetMyCompanysFollowers", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (followers) {
              controller.followers = followers;
          });

        Azureservice.invokeApi("GetMyCompanyFollowerGroups", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (followerGroups) {
              controller.followerGroups = followerGroups;
          });
    };

    controller.acceptRequest = function (companyConnection) {
        if (companyConnection.Status !== 0) {
            return;
        }

        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("AcceptCompanyConnection?companyConnectionId=" + companyConnection.ID, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function () {
              Azureservice.invokeApi("GetMyCompanysFollowers", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
                .then(function (followers) {
                    controller.followers = followers;
                });
          });
    };

    controller.getFollowerGroupDisplay = function (follower) {
        var len = follower.FollowerGroups.length;

        if (len === 0) {
            return "None";
        }

        var followerGroupDisplay = "";

        for (var i = 0; i < len && i < 3; i++) {
            followerGroupDisplay += follower.FollowerGroups[i].Name + ", ";
        }

        followerGroupDisplay = followerGroupDisplay.substring(0, followerGroupDisplay.length - 2);

        if (len <= 3) {
            return followerGroupDisplay;
        }

        followerGroupDisplay += " & " + (len - 3) + " more...";

        return followerGroupDisplay;
    };

    controller.editFollowerGroups = function (follower) {
        var editFollowerGroupsModalInstance = $uibModal.open({
            animation: true,
            ariaLabelledBy: "modal-title",
            ariaDescribedBy: "modal-body",
            templateUrl: "editFollowerGroupsModal.html",
            controller: "followerGroupsModalController",
            controllerAs: "ctrl",
            size: "sm",
            //appendTo: parentElem,
            resolve: {
                followerGroups: function () {
                    return angular.copy(controller.followerGroups);
                },
                follower: function () {
                    return follower;
                }
            }
        });

        editFollowerGroupsModalInstance.result.then(function (followerGroups) {
            var selectedFollowerGroupIds = [];

            for (var i = 0, len = followerGroups.length; i < len; i++) {
                if (followerGroups[i].Selected) {
                    selectedFollowerGroupIds.push(followerGroups[i].ID);
                }
            }

            var setFollowerFollowerGroupsDto = {
                FollowerUserID: follower.UserID,
                FollowerGroupIDs: selectedFollowerGroupIds
            };

            var authData = localStorageService.get("authorizationData");

            controller.resetNotifications();
            controller.busy = true;
            Azureservice.invokeApi("UpdateFollowerGroupsForFollower", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: setFollowerFollowerGroupsDto })
                .then(function () {
                    follower.FollowerGroups = [];

                    for (var i = 0, len = followerGroups.length; i < len; i++) {
                        if (followerGroups[i].Selected) {
                            follower.FollowerGroups.push(angular.copy(followerGroups[i]));
                        }
                    }

                    controller.showConfirmation("Follower groups set.");
                })
                .finally(function () {
                    controller.busy = false;
                });
        });
    };

    controller.declineRequest = function (companyConnection) {
        if (companyConnection.Status !== 0) {
            return;
        }

        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("RemoveCompanyConnection?companyConnectionId=" + companyConnection.ID, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function () {
              Azureservice.invokeApi("GetMyCompanysFollowers", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
                .then(function (followers) {
                    controller.followers = followers;
                });
          });
    };

    controller.removeConnection = function (companyConnection) {
        if (companyConnection.Status !== 1) {
            return;
        }

        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("RemoveCompanyConnection?companyConnectionId=" + companyConnection.ID, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function () {
              Azureservice.invokeApi("GetMyCompanysFollowers", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
                .then(function (followers) {
                    controller.followers = followers;
                });
          });
    };

    controller.getFollowerUserDisplayName = function (follower) {
        if (follower.UserFirstName && follower.UserLastName) {
            return follower.UserFirstName + " " + follower.UserLastName;
        }

        if (follower.UserFirstName) {
            return follower.UserFirstName;
        }

        if (follower.UserLastName) {
            return follower.UserLastName;
        }

        return follower.UserUserName;
    };

    controller.showError = function (errorMessage) {
        controller.errorMessage = errorMessage;
        controller.hasError = true;

        $timeout(function () {
            controller.errorMessage = null;
            controller.hasError = false;
        }, 15000);
        $("html, body").animate({
            scrollTop: 0
        }, 500);
    };

    controller.showConfirmation = function (confirmationMessage) {
        controller.confirmationMessage = confirmationMessage;
        controller.hasConfirmation = true;

        $timeout(function () {
            controller.confirmationMessage = null;
            controller.hasConfirmation = false;
        }, 15000);
        $("html, body").animate({
            scrollTop: 0
        }, 500);
    };

    controller.resetNotifications = function () {
        controller.errorMessage = null;
        controller.hasError = false;
        controller.confirmationMessage = null;
        controller.hasConfirmation = false;
    };

    controller.init();
}])
.controller("followerGroupsModalController", ["$uibModalInstance", "follower", "followerGroups", function ($uibModalInstance, follower, followerGroups) {
    var controller = this;

    controller.follower = follower;
    controller.followerGroups = followerGroups;

    controller.init = function () {
        for (var i = 0, len_i = controller.followerGroups.length; i < len_i; i++) {
            controller.followerGroups[i].Selected = false;

            for (var j = 0, len_j = controller.follower.FollowerGroups.length; j < len_j; j++) {
                if (controller.followerGroups[i].ID === controller.follower.FollowerGroups[j].ID) {
                    controller.followerGroups[i].Selected = true;

                    break;
                }
            }
        }
    };

    controller.ok = function () {
        $uibModalInstance.close(controller.followerGroups);
    };

    controller.cancel = function () {
        $uibModalInstance.dismiss();
    };

    controller.getFollowerUserDisplayName = function (follower) {
        if (follower.UserFirstName && follower.UserLastName) {
            return follower.UserFirstName + " " + follower.UserLastName;
        }

        if (follower.UserFirstName) {
            return follower.UserFirstName;
        }

        if (follower.UserLastName) {
            return follower.UserLastName;
        }

        return follower.UserUserName;
    };

    controller.init();
}])
.controller("productGroupsModalController", ["$uibModalInstance", "product", "followerGroups", function ($uibModalInstance, product, followerGroups) {
    var controller = this;

    controller.product = product;
    controller.followerGroups = followerGroups;

    controller.init = function () {
        for (var i = 0, len_i = controller.followerGroups.length; i < len_i; i++) {
            controller.followerGroups[i].Selected = false;

            for (var j = 0, len_j = controller.product.GroupsVisibleTo.length; j < len_j; j++) {
                if (controller.followerGroups[i].ID === controller.product.GroupsVisibleTo[j].ID) {
                    controller.followerGroups[i].Selected = true;

                    break;
                }
            }
        }
    };

    controller.ok = function () {
        $uibModalInstance.close(controller.followerGroups);
    };

    controller.cancel = function () {
        $uibModalInstance.dismiss();
    };

    controller.init();
}])
.controller("productFileGroupsModalController", ["$uibModalInstance", "productFile", "followerGroups", function ($uibModalInstance, productFile, followerGroups) {
    var controller = this;

    controller.productFile = productFile;
    controller.followerGroups = followerGroups;

    controller.init = function () {
        for (var i = 0, len_i = controller.followerGroups.length; i < len_i; i++) {
            controller.followerGroups[i].Selected = false;

            for (var j = 0, len_j = controller.productFile.GroupsVisibleTo.length; j < len_j; j++) {
                if (controller.followerGroups[i].ID === controller.productFile.GroupsVisibleTo[j].ID) {
                    controller.followerGroups[i].Selected = true;

                    break;
                }
            }
        }
    };

    controller.ok = function () {
        $uibModalInstance.close(controller.followerGroups);
    };

    controller.cancel = function () {
        $uibModalInstance.dismiss();
    };

    controller.init();
}])
.controller("productDetailsController", ["$scope", "Azureservice", "localStorageService", "$timeout", "$rootScope", "$stateParams", "ProductPrivacyEnum", "$uibModal", function ($scope, Azureservice, localStorageService, $timeout, $rootScope, $stateParams, ProductPrivacyEnum, $uibModal) {
    var controller = this;

    controller.productId = $stateParams.productId;
    controller.flowOptions = {
        target: "/api/ProductLogoUpload",
        permanentErrors: [500, 501],
        maxChunkRetries: 1,
        chunkRetryInterval: 2000,
        simultaneousUploads: 4,
        progressCallbacksInterval: 500,
        headers: function () {
            var authData = localStorageService.get("authorizationData");

            return { "Authorization": "Bearer " + authData.token };
        },
        generateUniqueIdentifier: function () {
            return (new Date()).getTime();
        }
    };
    controller.company = {};
    controller.products = [];
    controller.followerGroups = [];
    controller.product = {
        GroupsVisibleTo: []
    };
    controller.productCustomers = [];
    controller.productPrivacyEnum = ProductPrivacyEnum;
    controller.busy = false;
    controller.errorMessage = null;
    controller.hasError = false;
    controller.confirmationMessage = null;
    controller.hasConfirmation = false;

    controller.init = function () {
        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("GetMyCompany", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (company) {
              controller.company = company;
          });

        Azureservice.invokeApi("GetMyCompanyProducts", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (products) {
              controller.products = products;
          });

        Azureservice.invokeApi("GetMyCompanyFollowerGroups", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (followerGroups) {
              controller.followerGroups = followerGroups;
          });

        if (!controller.productId) {
            return;
        }

        Azureservice.invokeApi("GetProductForEditing?id=" + controller.productId, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (product) {
              controller.product = product;
          });

        Azureservice.invokeApi("GetProductCustomers?productId=" + controller.productId, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (productCustomers) {
              productCustomers = productCustomers.map(function (productCustomer) {
                  productCustomer.Editing = false;

                  return productCustomer;
              });
              controller.productCustomers = productCustomers;
          });
    };

    controller.updateProductUri = function () {
        if (!controller.product.Name) {
            controller.product.URI = "";
        }

        var productUri = controller.product.Name.replace(/ /g, "-");

        controller.product.URI = productUri.replace(/[^\w-_]+/g, "").toLowerCase();
    };

    controller.getProductGroupVisibilityDisplay = function () {
        var product = controller.product;

        if (!product.GroupsVisibleTo || !product.GroupsVisibleTo.length) {
            return "None";
        }

        var productGroupVisibilityDisplay = "";
        var len = product.GroupsVisibleTo.length;

        for (var i = 0; i < len && i < 3; i++) {
            productGroupVisibilityDisplay += product.GroupsVisibleTo[i].Name + ", ";
        }

        productGroupVisibilityDisplay = productGroupVisibilityDisplay.substring(0, productGroupVisibilityDisplay.length - 2);

        if (len <= 3) {
            return productGroupVisibilityDisplay;
        }

        productGroupVisibilityDisplay += " & " + (len - 3) + " more...";

        return productGroupVisibilityDisplay;
    };

    controller.editProductGroupVisibility = function (follower) {
        var editFollowerGroupsModalInstance = $uibModal.open({
            animation: true,
            ariaLabelledBy: "modal-title",
            ariaDescribedBy: "modal-body",
            templateUrl: "editProductGroupsVisbleToModal.html",
            controller: "productGroupsModalController",
            controllerAs: "ctrl",
            size: "sm",
            //appendTo: parentElem,
            resolve: {
                followerGroups: function () {
                    return angular.copy(controller.followerGroups);
                },
                product: function () {
                    return controller.product;
                }
            }
        });

        editFollowerGroupsModalInstance.result.then(function (followerGroups) {
            controller.product.GroupsVisibleTo = [];

            for (var i = 0, len = followerGroups.length; i < len; i++) {
                if (followerGroups[i].Selected) {
                    controller.product.GroupsVisibleTo.push(followerGroups[i]);
                }
            }

            return;
            var selectedFollowerGroupIds = [];

            for (var i = 0, len = followerGroups.length; i < len; i++) {
                if (followerGroups[i].Selected) {
                    selectedFollowerGroupIds.push(followerGroups[i].ID);
                }
            }

            var setFollowerFollowerGroupsDto = {
                FollowerUserID: follower.UserID,
                FollowerGroupIDs: selectedFollowerGroupIds
            };

            var authData = localStorageService.get("authorizationData");

            controller.resetNotifications();
            controller.busy = true;
            Azureservice.invokeApi("UpdateFollowerGroupsForFollower", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: setFollowerFollowerGroupsDto })
                .then(function () {
                    follower.FollowerGroups = [];

                    for (var i = 0, len = followerGroups.length; i < len; i++) {
                        if (followerGroups[i].Selected) {
                            follower.FollowerGroups.push(angular.copy(followerGroups[i]));
                        }
                    }

                    controller.showConfirmation("Follower groups set.");
                })
                .finally(function () {
                    controller.busy = false;
                });
        });
    };

    controller.getProductPrivacyLiteral = function () {
        for (var i = 0, len = controller.productPrivacyEnum.length; i < len; i++) {
            if (controller.product.Privacy === controller.productPrivacyEnum[i].code) {
                return controller.productPrivacyEnum[i].name;
            }
        }

        return "Please select";
    };

    controller.setProductPrivacy = function (productPrivacy) {
        controller.product.Privacy = productPrivacy.code;
    };

    controller.updateProductDetails = function () {
        if (!controller.productId) {
            return controller.addNewProduct();
        }

        var authData = localStorageService.get("authorizationData");

        controller.busy = true;
        controller.resetNotifications();
        Azureservice.invokeApi("UpdateProduct", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: controller.product })
            .then(function (product) {
                controller.product = product;
                controller.showConfirmation("Your changes were saved");
                Azureservice.invokeApi("GetMyCompanyProducts", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
                  .then(function (products) {
                      controller.products = products;
                  });
            })
            .finally(function () {
                controller.busy = false;
            });
    };

    controller.addNewProduct = function () {
        var authData = localStorageService.get("authorizationData");

        controller.busy = true;
        controller.resetNotifications();
        Azureservice.invokeApi("NewProduct", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: controller.product })
            .then(function (product) {
                controller.product = product;
                controller.productId = product.ID;
                controller.showConfirmation("Your changes were saved");
                Azureservice.invokeApi("GetMyCompanyProducts", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
                  .then(function (products) {
                      controller.products = products;
                  });
            })
            .finally(function () {
                controller.busy = false;
            });
    };

    controller.addNewProductCustomer = function (productCustomer) {
        var authData = localStorageService.get("authorizationData");

        controller.busy = true;
        controller.resetNotifications();
        Azureservice.invokeApi("NewCustomer", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: productCustomer })
            .then(function (productCustomer) {
                controller.showConfirmation("Added new customer logo");
                productCustomer.Editing = false;
                controller.productCustomers.push(productCustomer);
            })
            .finally(function () {
                controller.busy = false;
            });
    };

    controller.getProductLogo = function () {
        if (!controller.product.Logo) {
            return "http://placehold.it/212x212";
        }

        return "data:image/JPEG;base64," + controller.product.Logo;
    };

    controller.getProductCustomerLogo = function (productCustomer) {
        if (!productCustomer.Logo) {
            return "http://placehold.it/212x212";
        }

        return "data:image/JPEG;base64," + productCustomer.Logo;
    };

    controller.flowProductCustomerLogoImagesSubmitted = function ($files, $event, $flow) {
        if (!$event || !$files) {
            return;
        }

        var len = $files.length;

        if (!len) {
            return;
        }

        for (var i = 0; i < len; i++) {
            var reader = new FileReader();

            reader.addEventListener("load", function (reader) {
                var base64DataBeginMarker = ";base64,";
                var base64DataBeginIndex = reader.result.indexOf(base64DataBeginMarker);
                var productCustomer = {
                    ProductID: controller.productId,
                    Logo: reader.result.substring(base64DataBeginIndex + base64DataBeginMarker.length)
                };

                controller.addNewProductCustomer(productCustomer);
                $flow.cancel();
                $scope.$apply();
            }.bind(null, reader), false);
            reader.readAsDataURL($files[i].file);
        }
    };

    controller.flowProductLogoImageSubmitted = function ($files, $event, $flow) {
        if (!$event || !$files) {
            return;
        }

        var len = $files.length;

        if (!len) {
            return;
        }

        var reader = new FileReader();
        var base64DataBeginMarker = ";base64,";
        var base64DataBeginIndex;

        reader.addEventListener("load", function () {
            base64DataBeginIndex = reader.result.indexOf(base64DataBeginMarker);
            controller.product.Logo = reader.result.substring(base64DataBeginIndex + base64DataBeginMarker.length);
            $flow.cancel();
            $scope.$apply();
        }, false);
        reader.readAsDataURL($files[0].file);
    };

    controller.base64StringToBlob = function (base64String, contentType, sliceSize) {
        contentType = contentType || "";
        sliceSize = sliceSize || 512;

        var byteCharacters = atob(base64String);
        var byteArrays = [];
        var slice;
        var byteNumbers;
        var byteArray;
        var blob;

        for (var offset = 0; offset < byteCharacters.length; offset += sliceSize) {
            slice = byteCharacters.slice(offset, offset + sliceSize);
            byteNumbers = new Array(slice.length);

            for (var i = 0; i < slice.length; i++) {
                byteNumbers[i] = slice.charCodeAt(i);
            }

            byteArray = new Uint8Array(byteNumbers);
            byteArrays.push(byteArray);
        }

        blob = new Blob(byteArrays, { type: contentType });

        return blob;
    };

    controller.showError = function (errorMessage) {
        controller.errorMessage = errorMessage;
        controller.hasError = true;

        $timeout(function () {
            controller.errorMessage = null;
            controller.hasError = false;
        }, 15000);
        $("html, body").animate({
            scrollTop: 0
        }, 500);
    };

    controller.showConfirmation = function (confirmationMessage) {
        controller.confirmationMessage = confirmationMessage;
        controller.hasConfirmation = true;

        $timeout(function () {
            controller.confirmationMessage = null;
            controller.hasConfirmation = false;
        }, 15000);
        $("html, body").animate({
            scrollTop: 0
        }, 500);
    };

    controller.resetNotifications = function () {
        controller.errorMessage = null;
        controller.hasError = false;
        controller.confirmationMessage = null;
        controller.hasConfirmation = false;
    };

    controller.init();
}])
.controller("productTeamController", ["$scope", "Azureservice", "localStorageService", "$timeout", "$rootScope", "$stateParams", function ($scope, Azureservice, localStorageService, $timeout, $rootScope, $stateParams) {
    var controller = this;

    controller.productId = $stateParams.productId;
    controller.products = [];
    controller.productTeamMembers = [];
    controller.availableCompanyMembers = [];
    controller.errorMessage = null;
    controller.hasError = false;
    controller.confirmationMessage = null;
    controller.hasConfirmation = false;
    controller.busy = false;

    controller.init = function () {
        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("GetMyCompanyProducts", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (products) {
              controller.products = products;
          });

        if (!controller.productId) {
            return;
        }

        Azureservice.invokeApi("GetProductTeamMembersForEditing?productId=" + controller.productId, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (productTeamMembers) {
              controller.productTeamMembers = productTeamMembers;
          });

        Azureservice.invokeApi("GetAvailableTeamMembersForProduct?productId=" + controller.productId, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (availableCompanyMembers) {
              controller.availableCompanyMembers = availableCompanyMembers;
          });
    };

    controller.addNewProductTeamMember = function (companyMember) {
        var newProductTeamMember = {
            ProductID: controller.productId,
            UserID: companyMember.UserID
        };
        var authData = localStorageService.get("authorizationData");

        controller.busy = true;
        controller.resetNotifications();
        Azureservice.invokeApi("AddNewProductTeamMember", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: newProductTeamMember })
            .then(function (productTeamMember) { //TODO: Push product team member & pop company member instead of calling the back-end.
                controller.showConfirmation("Team member added");
                Azureservice.invokeApi("GetProductTeamMembersForEditing?productId=" + controller.productId, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
                 .then(function (productTeamMembers) {
                     controller.productTeamMembers = productTeamMembers;
                 });

                Azureservice.invokeApi("GetAvailableTeamMembersForProduct?productId=" + controller.productId, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
                  .then(function (availableCompanyMembers) {
                      controller.availableCompanyMembers = availableCompanyMembers;
                  });
            })
            .finally(function () {
                controller.busy = false;
            });
    };

    controller.removeProductTeamMember = function (productTeamMember) {
        var authData = localStorageService.get("authorizationData");

        controller.busy = true;
        controller.resetNotifications();
        Azureservice.invokeApi("RemoveProductTeamMember", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: productTeamMember })
            .then(function (productTeamMember) { //TODO: Push product team member & pop company member instead of calling the back-end.
                controller.showConfirmation("Team member removed");
                Azureservice.invokeApi("GetProductTeamMembersForEditing?productId=" + controller.productId, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
                 .then(function (productTeamMembers) {
                     controller.productTeamMembers = productTeamMembers;
                 });

                Azureservice.invokeApi("GetAvailableTeamMembersForProduct?productId=" + controller.productId, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
                  .then(function (availableCompanyMembers) {
                      controller.availableCompanyMembers = availableCompanyMembers;
                  });
            })
            .finally(function () {
                controller.busy = false;
            });
    };

    controller.updateProductTeamMember = function (productTeamMember) {
        var productTeamMemberUpdate = {
            ProductID: controller.productId,
            UserID: productTeamMember.UserID,
            Role: productTeamMember.Role,
            CanEditTheProduct: productTeamMember.CanEditTheProduct
        };
        var authData = localStorageService.get("authorizationData");

        controller.busy = true;
        controller.resetNotifications();
        Azureservice.invokeApi("UpdateProductTeamMember", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: productTeamMemberUpdate })
            .then(function (productTeamMember) {
                controller.showConfirmation("Team member updated");
            })
            .finally(function () {
                controller.busy = false;
            });
    };

    controller.setProductTeamMemberCanEditTheProduct = function (productTeamMember, canEditTheProduct) {
        productTeamMember.CanEditTheProduct = canEditTheProduct;
        controller.updateProductTeamMember(productTeamMember);
    };

    controller.getCompanyMemberUserDisplayName = function (companyMember) {
        if (companyMember.UserFirstName && companyMember.UserLastName) {
            return companyMember.UserFirstName + " " + companyMember.UserLastName;
        }

        if (companyMember.UserFirstName) {
            return companyMember.UserFirstName;
        }

        if (companyMember.UserLastName) {
            return companyMember.UserLastName;
        }

        return companyMember.UserUserName;
    };

    controller.getProductTeamMemberUserDisplayName = function (productMember) {
        if (productMember.UserFirstName && productMember.UserLastName) {
            return productMember.UserFirstName + " " + productMember.UserLastName;
        }

        if (productMember.UserFirstName) {
            return productMember.UserFirstName;
        }

        if (productMember.UserLastName) {
            return productMember.UserLastName;
        }

        return productMember.UserUserName;
    };

    controller.showError = function (errorMessage) {
        controller.errorMessage = errorMessage;
        controller.hasError = true;

        $timeout(function () {
            controller.errorMessage = null;
            controller.hasError = false;
        }, 15000);
        $("html, body").animate({
            scrollTop: 0
        }, 500);
    };

    controller.showConfirmation = function (confirmationMessage) {
        controller.confirmationMessage = confirmationMessage;
        controller.hasConfirmation = true;

        $timeout(function () {
            controller.confirmationMessage = null;
            controller.hasConfirmation = false;
        }, 15000);
        $("html, body").animate({
            scrollTop: 0
        }, 500);
    };

    controller.resetNotifications = function () {
        controller.errorMessage = null;
        controller.hasError = false;
        controller.confirmationMessage = null;
        controller.hasConfirmation = false;
    };

    controller.init();
}])
.controller("productFilesController", ["$scope", "Azureservice", "localStorageService", "$timeout", "$rootScope", "$stateParams", "ProductFileCategories", "ProductFilePrivacyEnum", "$uibModal", function ($scope, Azureservice, localStorageService, $timeout, $rootScope, $stateParams, ProductFileCategories, ProductFilePrivacyEnum, $uibModal) {
    var controller = this;

    controller.productId = $stateParams.productId;
    controller.products = [];
    controller.followerGroups = [];
    controller.product = {};
    controller.productFileCategories = ProductFileCategories;
    controller.productFilesByCategory = [];
    controller.productFilePrivacyEnum = ProductFilePrivacyEnum;
    controller.selectedFollowerGroup = null;
    controller.busy = false;
    controller.errorMessage = null;
    controller.hasError = false;
    controller.confirmationMessage = null;
    controller.hasConfirmation = false;

    controller.init = function () {
        var authData = localStorageService.get("authorizationData");

        for (var i = 0, len = controller.productFileCategories.length; i < len; i++) {
            controller.productFilesByCategory.push({
                category: controller.productFileCategories[i],
                productFiles: []
            });
        }

        Azureservice.invokeApi("GetMyCompanyProducts", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (products) {
              controller.products = products;
          });

        Azureservice.invokeApi("GetMyCompanyFollowerGroups", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (followerGroups) {
              controller.followerGroups = followerGroups;
          });

        if (!controller.productId) {
            return;
        }

        Azureservice.invokeApi("GetProductForEditing?id=" + controller.productId, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (product) {
              controller.product = product;
              $scope.$broadcast("dataready", null);
          });

        Azureservice.invokeApi("GetProductFilesForEditing?productId=" + controller.productId + "&followerGroupId=" + (controller.selectedFollowerGroup === null ? "null" : controller.selectedFollowerGroup.ID), { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (productFiles) {
              for (var i = 0, len_i = controller.productFilesByCategory.length; i < len_i; i++) {
                  controller.productFilesByCategory[i].productFiles = [];

                  for (var j = 0, len_j = productFiles.length; j < len_j; j++) {
                      if (productFiles[j].Category === controller.productFilesByCategory[i].category.code) {
                          controller.productFilesByCategory[i].productFiles.push(productFiles[j]);
                      }
                  }
              }
          });
    };

    controller.selectFollowerGroup = function (followerGroup) {
        if (followerGroup === null && controller.selectedFollowerGroup === null) {
            return;
        }

        if (followerGroup !== null && controller.selectedFollowerGroup !== null && followerGroup.ID === controller.selectedFollowerGroup.ID) {
            return;
        }

        controller.selectedFollowerGroup = followerGroup;

        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("GetProductFilesForEditing?productId=" + controller.productId + "&followerGroupId=" + (controller.selectedFollowerGroup === null ? "null" : controller.selectedFollowerGroup.ID), { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (productFiles) {
              for (var i = 0, len_i = controller.productFilesByCategory.length; i < len_i; i++) {
                  controller.productFilesByCategory[i].productFiles = [];

                  for (var j = 0, len_j = productFiles.length; j < len_j; j++) {
                      if (productFiles[j].Category === controller.productFilesByCategory[i].category.code) {
                          controller.productFilesByCategory[i].productFiles.push(productFiles[j]);
                      }
                  }
              }
          });
    };

    controller.getProductFileGroupVisibilityDisplay = function (productFile) {
        if (!productFile.GroupsVisibleTo || !productFile.GroupsVisibleTo.length) {
            return "None";
        }

        var productFileGroupVisibilityDisplay = "";
        var len = productFile.GroupsVisibleTo.length;

        for (var i = 0; i < len && i < 3; i++) {
            productFileGroupVisibilityDisplay += productFile.GroupsVisibleTo[i].Name + ", ";
        }

        productFileGroupVisibilityDisplay = productFileGroupVisibilityDisplay.substring(0, productFileGroupVisibilityDisplay.length - 2);

        if (len <= 3) {
            return productFileGroupVisibilityDisplay;
        }

        productFileGroupVisibilityDisplay += " & " + (len - 3) + " more...";

        return productFileGroupVisibilityDisplay;
    };

    controller.editProductFileGroupVisibility = function (productFile) {
        var editFollowerGroupsModalInstance = $uibModal.open({
            animation: true,
            ariaLabelledBy: "modal-title",
            ariaDescribedBy: "modal-body",
            templateUrl: "editProductFileGroupsVisbleToModal.html",
            controller: "productFileGroupsModalController",
            controllerAs: "ctrl",
            size: "sm",
            resolve: {
                followerGroups: function () {
                    return angular.copy(controller.followerGroups);
                },
                productFile: function () {
                    return productFile;
                }
            }
        });

        editFollowerGroupsModalInstance.result.then(function (followerGroups) {
            var selectedFollowerGroupIds = [];

            for (var i = 0, len = followerGroups.length; i < len; i++) {
                if (followerGroups[i].Selected) {
                    selectedFollowerGroupIds.push(followerGroups[i].ID);
                }
            }

            var groupsVisibleToForProductFile = {
                ProductFileID: productFile.ID,
                FollowerGroupIDs: selectedFollowerGroupIds
            };

            var authData = localStorageService.get("authorizationData");

            controller.resetNotifications();
            controller.busy = true;
            Azureservice.invokeApi("UpdateGroupsVisibleToForProductFile", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: groupsVisibleToForProductFile })
                .then(function () {
                    productFile.GroupsVisibleTo = [];

                    for (var i = 0, len = followerGroups.length; i < len; i++) {
                        if (followerGroups[i].Selected) {
                            productFile.GroupsVisibleTo.push(followerGroups[i]);
                        }
                    }

                    controller.showConfirmation("Follower groups set.");
                })
                .finally(function () {
                    controller.busy = false;
                });
        });
    };

    controller.getProductFilePrivacyLiteral = function (productFile) {
        for (var i = 0, len = controller.productFilePrivacyEnum.length; i < len; i++) {
            if (productFile.Privacy === controller.productFilePrivacyEnum[i].code) {
                return controller.productFilePrivacyEnum[i].name;
            }
        }

        return "Please select";
    };

    controller.setProductFilePrivacy = function (productFile, productFilePrivacy) {
        if (productFile.Privacy === productFilePrivacy.code) {
            return;
        }

        var authData = localStorageService.get("authorizationData");

        controller.busy = true;
        Azureservice.invokeApi("SetProductFilePrivacy?id=" + productFile.ID + "&privacy=" + productFilePrivacy.code, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
            .then(function () {
                productFile.Privacy = productFilePrivacy.code;

                if (productFile.Privacy !== 3) {
                    productFile.GroupsVisibleTo = [];
                }

                controller.showConfirmation("File privacy set.");
            })
            .finally(function () {
                controller.busy = false;
            });
    };

    controller.setProductFileCategory = function (productFile, productFileCategory) {
        if (productFile.Category === productFileCategory.code) {
            return;
        }

        var authData = localStorageService.get("authorizationData");

        controller.busy = true;
        Azureservice.invokeApi("SetProductFileCategory?id=" + productFile.ID + "&category=" + productFileCategory.code, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
            .then(function () {
                Azureservice.invokeApi("GetProductFilesForEditing?productId=" + controller.productId + "&followerGroupId=" + (controller.selectedFollowerGroup === null ? "null" : controller.selectedFollowerGroup.ID), { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
                 .then(function (productFiles) {
                     for (var i = 0, len_i = controller.productFilesByCategory.length; i < len_i; i++) {
                         controller.productFilesByCategory[i].productFiles = [];

                         for (var j = 0, len_j = productFiles.length; j < len_j; j++) {
                             if (productFiles[j].Category === controller.productFilesByCategory[i].category.code) {
                                 controller.productFilesByCategory[i].productFiles.push(productFiles[j]);
                             }
                         }
                     }
                     controller.showConfirmation("File category set.");
                 });
            })
            .finally(function () {
                controller.busy = false;
            });
    };

    controller.getProductFileCategoryName = function (productFile, valueIfNull) {
        for (var i = 0, len = controller.productFileCategories.length; i < len; i++) {
            if (controller.productFileCategories[i].code === productFile.Category) {
                return controller.productFileCategories[i].name;
            }
        }

        return valueIfNull;
    };

    controller.deleteProductFile = function (productFile) {
        var authData = localStorageService.get("authorizationData");

        controller.busy = true;
        Azureservice.invokeApi("DeleteProductFile?id=" + productFile.ID, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
            .then(function () {
                Azureservice.invokeApi("GetProductFilesForEditing?productId=" + controller.productId + "&followerGroupId=" + (controller.selectedFollowerGroup === null ? "null" : controller.selectedFollowerGroup.ID), { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
                 .then(function (productFiles) {
                     for (var i = 0, len_i = controller.productFilesByCategory.length; i < len_i; i++) {
                         controller.productFilesByCategory[i].productFiles = [];

                         for (var j = 0, len_j = productFiles.length; j < len_j; j++) {
                             if (productFiles[j].Category === controller.productFilesByCategory[i].category.code) {
                                 controller.productFilesByCategory[i].productFiles.push(productFiles[j]);
                             }
                         }
                     }
                     $scope.$broadcast("dataready", null);
                 });
            })
            .finally(function () {
                controller.busy = false;
            });
    };

    controller.showError = function (errorMessage) {
        controller.errorMessage = errorMessage;
        controller.hasError = true;

        $timeout(function () {
            controller.errorMessage = null;
            controller.hasError = false;
        }, 15000);
        $("html, body").animate({
            scrollTop: 0
        }, 500);
    };

    controller.showConfirmation = function (confirmationMessage) {
        controller.confirmationMessage = confirmationMessage;
        controller.hasConfirmation = true;

        $timeout(function () {
            controller.confirmationMessage = null;
            controller.hasConfirmation = false;
        }, 15000);
        $("html, body").animate({
            scrollTop: 0
        }, 500);
    };

    controller.resetNotifications = function () {
        controller.errorMessage = null;
        controller.hasError = false;
        controller.confirmationMessage = null;
        controller.hasConfirmation = false;
    };

    controller.init();
}])
.controller("productFileUploadController", ["$scope", "Azureservice", "localStorageService", "$rootScope", "ProductFileCategories", "$state", "$stateParams", "$http", function ($scope, Azureservice, localStorageService, $rootScope, ProductFileCategories, $state, $stateParams, $http) {
    var controller = this;

    controller.productId = $stateParams.productId;
    controller.flowOptions = {
        target: "/api/ProductFileUpload",
        permanentErrors: [500, 501],
        maxChunkRetries: 1,
        chunkRetryInterval: 2000,
        simultaneousUploads: 4,
        progressCallbacksInterval: 500,
        headers: function () {
            var authData = localStorageService.get("authorizationData");

            return { "Authorization": "Bearer " + authData.token };
        },
        generateUniqueIdentifier: function () {
            return (new Date()).getTime();
        }
    };
    controller.validationErrors = [];
    controller.products = [];
    controller.productFileCategories = ProductFileCategories;
    controller.productFiles = [];
    controller.productFileIndex = 0;
    controller.uploadUrl = null;
    controller.showUrlUploadPopover = false;
    controller.busy = false;
    controller.errorMessage = null;
    controller.hasError = false;
    controller.confirmationMessage = null;
    controller.hasConfirmation = false;

    controller.init = function () {
        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("GetMyCompanyProducts", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (products) {
              controller.products = products;
              $scope.$broadcast("dataready", null);
          });

        //$('[data-toggle="popover"]').popover()
    };

    controller.toggleUrlUploadPopover = function () {
        controller.showUrlUploadPopover = !controller.showUrlUploadPopover;
    };

    controller.arrayBufferToBase64String = function (arrayBuffer) {
        var binary = "";
        var bytes = new Uint8Array(arrayBuffer);
        var len = bytes.byteLength;

        for (var i = 0; i < len; i++) {
            binary += String.fromCharCode(bytes[i]);
        }

        return window.btoa(binary);
    }

    controller.getFileFromUrl = function () {
        if (!controller.uploadUrl || controller.uploadUrl.length === 0) {
            return;
        }

        var authData = localStorageService.get("authorizationData");

        controller.busy = true;
        $http.get("/api/GetFileFromUrl?url=" + encodeURIComponent(controller.uploadUrl), {
            responseType: "arraybuffer"
        }).success(function (data, status, headers) {
            var base64 = controller.arrayBufferToBase64String(data);
            var contentType = headers["content-type"] || "application/octet-stream";
            var productFile = {
                Index: controller.productFileIndex++,
                Name: controller.uploadUrl,
                //Type: file.type,
                Size: base64.length,
                Image: null,
                ImageType: null,
                Editing: false,
                Bytes: base64
            };

            controller.productFiles.push(productFile);
            controller.showUrlUploadPopover = false;
            controller.uploadUrl = null;
            controller.busy = false;
        });
    };

    controller.uploadProductFiles = function () {
        var authData = localStorageService.get("authorizationData");

        controller.busy = true;
        Azureservice.invokeApi("UploadProductFiles", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: controller.productFiles })
            .then(function (productFiles) {
                controller.productFiles = productFiles;
                $scope.$broadcast("dataready", null);

                if ($rootScope.previousState) {
                    $state.go($rootScope.previousState.name);
                }
                else {
                    $state.go("dashboard");
                }
            })
            .finally(function () {
                controller.busy = false;
            });
    };

    controller.getProductFileImage = function (productFile) {
        
        if (!productFile.Image) {
            return "http://placehold.it/212x212";
        }

        return "data:image/JPEG;base64," + productFile.Image;
    };

    controller.flowProductFilesSubmitted = function ($files, $event, $flow) {
        if (!$event || !$files) {
            return;
        }

        var len = $files.length;

        if (!len) {
            return;
        }

        for (var i = 0; i < len; i++) {
            var reader = new FileReader();

            reader.addEventListener("load", function (file) {
                var base64DataBeginMarker = ";base64,";
                var base64DataBeginIndex = reader.result.indexOf(base64DataBeginMarker);
                var productFile = {
                    Index: controller.productFileIndex++,
                    Name: file.name,
                    Type: file.type,
                    Size: file.size,
                    Image: null,
                    ImageType: null,
                    Editing: false,
                    Bytes: reader.result.substring(base64DataBeginIndex + base64DataBeginMarker.length)
                };

                controller.productFiles.push(productFile);
                $flow.cancel();
                $scope.$apply();
            }.bind(null, $files[i].file), false);
            reader.readAsDataURL($files[i].file);
        }
    };

    controller.flowProductFileImageSubmitted = function ($files, $event, $flow, productFile) {
        if (!$event || !$files) {
            return;
        }

        var len = $files.length;

        if (!len) {
            return;
        }

        var reader = new FileReader();
        var base64DataBeginMarker = ";base64,";
        var base64DataBeginIndex;

        reader.addEventListener("load", function () {
            base64DataBeginIndex = reader.result.indexOf(base64DataBeginMarker);
            productFile.Image = reader.result.substring(base64DataBeginIndex + base64DataBeginMarker.length);
            productFile.ImageType = $files[0].getType();
            $flow.cancel();
            $scope.$apply();
        }, false);
        reader.readAsDataURL($files[0].file);
    };

    controller.base64StringToBlob = function (base64String, contentType, sliceSize) {
        contentType = contentType || "";
        sliceSize = sliceSize || 512;

        var byteCharacters = atob(base64String);
        var byteArrays = [];
        var slice;
        var byteNumbers;
        var byteArray;
        var blob;

        for (var offset = 0; offset < byteCharacters.length; offset += sliceSize) {
            slice = byteCharacters.slice(offset, offset + sliceSize);
            byteNumbers = new Array(slice.length);

            for (var i = 0; i < slice.length; i++) {
                byteNumbers[i] = slice.charCodeAt(i);
            }

            byteArray = new Uint8Array(byteNumbers);
            byteArrays.push(byteArray);
        }

        blob = new Blob(byteArrays, { type: contentType });

        return blob;
    };

    controller.removeProductFile = function (productFile) {
        for (var i = 0, len = controller.productFiles.length; i < len; i++) {
            if (controller.productFiles[i].Index === productFile.Index) {
                controller.productFiles.splice(i, 1);

                return;
            }
        }
    };

    controller.toggleEditProductFile = function (productFile) {
        productFile.Editing = !productFile.Editing;
    };

    controller.setProductFileCategory = function (productFileCategory, productFile) {
        productFile.Category = productFileCategory.code;
    };

    controller.getProductFileCategoryName = function (productFile, valueIfNull) {
        for (var i = 0, len = controller.productFileCategories.length; i < len; i++) {
            if (controller.productFileCategories[i].code === productFile.Category) {
                return controller.productFileCategories[i].name;
            }
        }

        return valueIfNull;
    };

    controller.setProductFileProduct = function (product, productFile) {
        productFile.ProductID = product.ID;
    };

    controller.getProductFileProductName = function (productFile, valueIfNull) {
        if (!productFile.ProductID) {
            return valueIfNull;
        }

        for (var i = 0, len = controller.products.length; i < len; i++) {
            if (controller.products[i].ID === productFile.ProductID) {
                return controller.products[i].Name;
            }
        }

        return valueIfNull;
    };

    controller.init();
}])
.controller("dashboardController", ["$scope", "Azureservice", "localStorageService", "$stateParams", function ($scope, Azureservice, localStorageService, $stateParams) {
    var controller = this;

    controller.editing = false;
    controller.products = [];
    controller.articles = [];
    controller.newArticleProduct = null;
    controller.newArticleBody = null;
    controller.newArticleLinkUrl = null;
    controller.editingNewArticleLinkUrl = false;
    controller.newArticle = {};
    controller.pageNumber = 1;
    controller.pageSize = 10;
    controller.companyStatistics = {};
    controller.notificationCounters = {};
    controller.flowOptions = {
        target: "/api/ProductFileUpload",
        permanentErrors: [500, 501],
        maxChunkRetries: 1,
        chunkRetryInterval: 2000,
        simultaneousUploads: 4,
        progressCallbacksInterval: 500,
        headers: function () {
            var authData = localStorageService.get("authorizationData");

            return { "Authorization": "Bearer " + authData.token };
        },
        generateUniqueIdentifier: function () {
            return (new Date()).getTime();
        }
    };
    controller.busy = false;
    controller.errorMessage = null;
    controller.hasError = false;
    controller.confirmationMessage = null;
    controller.hasConfirmation = false;

    controller.init = function () {
        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("GetMyCompanyStatistics", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (companyStatistics) {
              controller.companyStatistics = companyStatistics;
          });

        Azureservice.invokeApi("GetMyNotificationCounters", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (notificationCounters) {
              controller.notificationCounters = notificationCounters;
          });

        Azureservice.invokeApi("GetMyCompanyProducts", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (products) {
              controller.products = products;
              $scope.$broadcast("dataready", null);
          });

        Azureservice.invokeApi("GetLatestNewsItemsPaged?pageSize=" + controller.pageSize + "&pageNumber=" + controller.pageNumber, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (articles) {
              controller.articles = articles;
          });

        if ($stateParams.showArticleEditor) {
            controller.editing = true;
        }
    };

    controller.toggleEditNewArticleLinkUrl = function () {
        controller.editingNewArticleLinkUrl = !controller.editingNewArticleLinkUrl;
    };

    controller.toggleEditNewArticleFeedbackLinkUrl = function (article) {
        if (!article.EditingLinkUrl) {
            article.EditingLinkUrl = true;

            return;
        }

        article.EditingLinkUrl = !article.EditingLinkUrl;
    };

    controller.setNewArticleLinkUrl = function () {
        controller.editingNewArticleLinkUrl = false;
    };

    controller.setNewArticleFeedbackLinkUrl = function (article) {
        article.EditingLinkUrl = false;
    };

    controller.getNewArticleImage = function () {
        if (!controller.newArticle.Image) {
            return "http://placehold.it/212x212";
        }

        return "data:image/JPEG;base64," + controller.newArticle.Image;
    };

    controller.getNewFeedbackImage = function (article) {
        if (!article.newFeedbackImage) {
            return "http://placehold.it/400x400";
        }

        return "data:image/JPEG;base64," + article.newFeedbackImage;
    };

    controller.flowNewArticleImageSubmitted = function ($files, $event, $flow) {
        if (!$event || !$files) {
            return;
        }

        var len = $files.length;

        if (!len) {
            return;
        }

        var reader = new FileReader();
        var base64DataBeginMarker = ";base64,";
        var base64DataBeginIndex;

        reader.addEventListener("load", function () {
            base64DataBeginIndex = reader.result.indexOf(base64DataBeginMarker);
            controller.newArticle.Image = reader.result.substring(base64DataBeginIndex + base64DataBeginMarker.length);
            $flow.cancel();
            $scope.$apply();
        }, false);
        reader.readAsDataURL($files[0].file);
    };

    controller.flowNewArticleFeedbackImageSubmitted = function ($files, $event, $flow, article) {
        if (!$event || !$files) {
            return;
        }

        var len = $files.length;

        if (!len) {
            return;
        }

        var reader = new FileReader();
        var base64DataBeginMarker = ";base64,";
        var base64DataBeginIndex;

        reader.addEventListener("load", function () {
            base64DataBeginIndex = reader.result.indexOf(base64DataBeginMarker);
            article.newFeedbackImage = reader.result.substring(base64DataBeginIndex + base64DataBeginMarker.length);
            $flow.cancel();
            $scope.$apply();
        }, false);
        reader.readAsDataURL($files[0].file);
    };

    controller.base64StringToBlob = function (base64String, contentType, sliceSize) {
        contentType = contentType || "";
        sliceSize = sliceSize || 512;

        var byteCharacters = atob(base64String);
        var byteArrays = [];
        var slice;
        var byteNumbers;
        var byteArray;
        var blob;

        for (var offset = 0; offset < byteCharacters.length; offset += sliceSize) {
            slice = byteCharacters.slice(offset, offset + sliceSize);
            byteNumbers = new Array(slice.length);

            for (var i = 0; i < slice.length; i++) {
                byteNumbers[i] = slice.charCodeAt(i);
            }

            byteArray = new Uint8Array(byteNumbers);
            byteArrays.push(byteArray);
        }

        blob = new Blob(byteArrays, { type: contentType });

        return blob;
    };

    controller.getHeightForStatsBar = function (maxHeight, value, comparativeValue) {
        if (!value) {
            return 0;
        }

        if (value > comparativeValue) {
            return maxHeight;
        }

        return Math.floor(maxHeight * value / comparativeValue);
    }

    controller.enableEdit = function () {
        controller.editing = true;
    };

    controller.setNewArticleProduct = function (product) {
        controller.newArticleProduct = product;
    };

    controller.postNewArticle = function () {
        if (!controller.newArticleBody || controller.newArticleBody.length === 0) {
            return;
        }

        var authData = localStorageService.get("authorizationData");

        controller.newArticle.Title = "News post";
        controller.newArticle.Body = controller.newArticleBody;
        controller.newArticle.LinkUrl = controller.newArticleLinkUrl;
        controller.busy = true;

        if (controller.newArticleProduct) {
            controller.newArticle.ProductID = controller.newArticleProduct.ID;
        }

        Azureservice.invokeApi("NewNewsItem", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: controller.newArticle })
            .then(function (article) {
                controller.articles.push(article);
                controller.newArticle = {};
                controller.newArticleProduct = null;
                controller.newArticleBody = null;
                controller.newArticleLinkUrl = null;
                controller.editing = false;
            })
            .finally(function () {
                controller.busy = false;
            });
    };

    controller.postNewFeedbackForArticle = function (article) {
        if (!article.newFeedbackBody || article.newFeedbackBody.length === 0) {
            return;
        }

        var authData = localStorageService.get("authorizationData");
        var newFeedback = {
            NewsItemID: article.ID,
            Title: "Feedback",
            Body: article.newFeedbackBody,
            LinkUrl: article.newFeedbackLinkUrl,
            Image: article.newFeedbackImage
        };

        controller.busy = true;
        Azureservice.invokeApi("NewNewsItemFeedback", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: newFeedback })
            .then(function (feedback) {
                article.Feedback.push(feedback);
                article.UpdatedAt = feedback.PostedAt;
                article.newFeedbackBody = null;
                article.newFeedbackLinkUrl = null;
                article.newFeedbackImage = null;
            })
            .finally(function () {
                controller.busy = false;
            });
    };

    controller.getArticleDisplayDate = function (article) {
        if (!article.PostedAt) {
            return null;
        }

        var date = moment(article.PostedAt);

        if (!date) {
            return null;
        }

        var now = moment();
        var difference = moment.duration(now.diff(date));
        var differenceInMinutes = difference.asMinutes();

        if (differenceInMinutes < 1) {
            return "just now";
        }

        if (differenceInMinutes < 59) {
            return Math.round(differenceInMinutes) + " min";
        }

        var differenceInHours = difference.asHours();

        if (differenceInHours < 24) {
            return Math.round(differenceInHours) + " hours";
        }

        var differenceInDays = difference.asDays();

        if (differenceInDays < 7) {
            return Math.round(differenceInDays) + " days";
        }

        return date.format("DD MMM YY");
    };

    controller.getArticleUserDisplayName = function (article) {
        if (article.UserFirstName && article.UserLastName) {
            return article.UserFirstName + " " + article.UserLastName;
        }

        if (article.UserFirstName) {
            return article.UserFirstName
        }

        if (article.UserLastName) {
            return article.UserLastName;
        }

        return article.UserUserName;
    };

    controller.getFeedbackDisplayDate = function (feedback) {
        if (!feedback.PostedAt) {
            return null;
        }

        var date = moment(feedback.PostedAt);

        if (!date) {
            return null;
        }

        var now = moment();
        var difference = moment.duration(now.diff(date));
        var differenceInMinutes = difference.asMinutes();

        if (differenceInMinutes < 1) {
            return "(just now)";
        }

        if (differenceInMinutes < 59) {
            return "(" + Math.round(differenceInMinutes) + " minutes ago)";
        }

        var differenceInHours = difference.asHours();

        if (differenceInHours < 24) {
            return "(" + Math.round(differenceInHours) + " hours ago)";
        }

        var differenceInDays = difference.asDays();

        if (differenceInDays < 7) {
            return "(" + Math.round(differenceInDays) + " days ago)";
        }

        return "(" + date.format("DD MMM YY") + ")";
    };

    controller.init();
}])
.controller("dashboardFeedbackController", ["$scope", "Azureservice", "localStorageService", function ($scope, Azureservice, localStorageService) {
    var controller = this;

    controller.productFileFeedback = [];
    controller.pageNumber = 1;
    controller.pageSize = 10;
    controller.selectedFeedback = null;
    controller.productFileFeedbackBody = null;
    controller.productFileFeedbackLinkUrl = null;
    controller.productFileFeedbackImage = null;
    controller.editingProductFileFeedbackLinkUrl = false;
    controller.companyStatistics = {};
    controller.notificationCounters = {};
    controller.flowOptions = {
        permanentErrors: [500, 501],
        maxChunkRetries: 1,
        chunkRetryInterval: 2000,
        simultaneousUploads: 4,
        progressCallbacksInterval: 500,
        generateUniqueIdentifier: function () {
            return (new Date()).getTime();
        }
    };
    controller.busy = false;
    controller.errorMessage = null;
    controller.hasError = false;
    controller.confirmationMessage = null;
    controller.hasConfirmation = false;

    controller.init = function () {
        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("GetMyCompanyStatistics", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (companyStatistics) {
              controller.companyStatistics = companyStatistics;
          });

        Azureservice.invokeApi("GetMyNotificationCounters", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (notificationCounters) {
              controller.notificationCounters = notificationCounters;
          });

        Azureservice.invokeApi("GetLatestProductFileFeedbackPaged?pageSize=" + controller.pageSize + "&pageNumber=" + controller.pageNumber, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (productFileFeedback) {
              controller.productFileFeedback = productFileFeedback;

              if (controller.productFileFeedback.length === 0) {
                  return;
              }

              for (var i = 0, len = controller.productFileFeedback.length; i < len; i++) {
                  controller.productFileFeedback[i].Active = false;
              }

              controller.selectFeedback(controller.productFileFeedback[0]);
          });
    };

    controller.toggleEditProductFileFeedbackLinkUrl = function () {
        controller.editingProductFileFeedbackLinkUrl = !controller.editingProductFileFeedbackLinkUrl;
    };

    controller.setProductFileFeedbackLinkUrl = function () {
        controller.editingProductFileFeedbackLinkUrl = false;
    };

    controller.getProductFileFeedbackImage = function () {
        if (!controller.productFileFeedbackImage) {
            return "http://placehold.it/212x212";
        }

        return "data:image/JPEG;base64," + controller.productFileFeedbackImage;
    };

    controller.flowProductFileFeedbackImageSubmitted = function ($files, $event, $flow) {
        if (!$event || !$files) {
            return;
        }

        var len = $files.length;

        if (!len) {
            return;
        }

        var reader = new FileReader();
        var base64DataBeginMarker = ";base64,";
        var base64DataBeginIndex;

        reader.addEventListener("load", function () {
            base64DataBeginIndex = reader.result.indexOf(base64DataBeginMarker);
            controller.productFileFeedbackImage = reader.result.substring(base64DataBeginIndex + base64DataBeginMarker.length);
            $flow.cancel();
            $scope.$apply();
        }, false);
        reader.readAsDataURL($files[0].file);
    };

    controller.getHeightForStatsBar = function (maxHeight, value, comparativeValue) {
        if (!value) {
            return 0;
        }

        if (value > comparativeValue) {
            return maxHeight;
        }

        return Math.floor(maxHeight * value / comparativeValue);
    }

    controller.selectFeedback = function (feedback) {
        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("GetProductFileFeedbackReplies?productFileFeedbackId=" + feedback.ID, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (productFileFeedbackReplies) {
              controller.productFileFeedbackReplies = productFileFeedbackReplies;

              if (controller.selectedFeedback) {
                  controller.selectedFeedback.Active = false;
              }

              feedback.Active = true;
              controller.selectedFeedback = feedback;
          });
    };

    controller.postProductFileFeedback = function () {
        if (!controller.selectedFeedback || !controller.productFileFeedbackBody || controller.productFileFeedbackBody.length === 0) {
            return;
        }

        var authData = localStorageService.get("authorizationData");
        var newProductFileFeedback = {
            ProductFileID: controller.selectedFeedback.ProductFileID,
            ReplyToID: controller.selectedFeedback.ID,
            Title: controller.selectedFeedback.Title,
            Body: controller.productFileFeedbackBody,
            LinkUrl: controller.productFileFeedbackLinkUrl,
            Image: controller.productFileFeedbackImage
        };

        controller.busy = true;
        Azureservice.invokeApi("NewProductFileFeedback", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: newProductFileFeedback })
            .then(function (productFileFeedback) {
                controller.productFileFeedback.push(productFileFeedback);
                controller.selectedFeedback.UpdatedAt = productFileFeedback.PostedAt;
                controller.productFileFeedbackBody = null;
                controller.productFileFeedbackLinkUrl = null;
                controller.productFileFeedbackImage = null;
                Azureservice.invokeApi("GetProductFileFeedbackReplies?productFileFeedbackId=" + controller.selectedFeedback.ID, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
                  .then(function (productFileFeedbackReplies) {
                      controller.productFileFeedbackReplies = productFileFeedbackReplies;
                  });
            })
            .finally(function () {
                controller.busy = false;
            });
    };

    controller.getArticleUserDisplayName = function (article) {
        if (article.UserFirstName && article.UserLastName) {
            return article.UserFirstName + " " + article.UserLastName;
        }

        if (article.UserFirstName) {
            return article.UserFirstName
        }

        if (article.UserLastName) {
            return article.UserLastName;
        }

        return article.UserUserName;
    };

    controller.getFeedbackDisplayDate = function (feedback) {
        if (!feedback.PostedAt) {
            return null;
        }

        var date = moment(feedback.PostedAt);

        if (!date) {
            return null;
        }

        var now = moment();
        var difference = moment.duration(now.diff(date));
        var differenceInMinutes = difference.asMinutes();

        if (differenceInMinutes < 1) {
            return "(just now)";
        }

        if (differenceInMinutes < 59) {
            return "(" + Math.round(differenceInMinutes) + " minutes ago)";
        }

        var differenceInHours = difference.asHours();

        if (differenceInHours < 24) {
            return "(" + Math.round(differenceInHours) + " hours ago)";
        }

        var differenceInDays = difference.asDays();

        if (differenceInDays < 7) {
            return "(" + Math.round(differenceInDays) + " days ago)";
        }

        return "(" + date.format("DD MMM YY") + ")";
    };

    controller.init();
}])
.controller("dashboardProductUpdatesController", ["$scope", "Azureservice", "localStorageService", "ProductUpdateTypes", function ($scope, Azureservice, localStorageService, ProductUpdateTypes) {
    var controller = this;

    controller.productUpdates = [];
    controller.pageNumber = 1;
    controller.pageSize = 10;
    controller.companyStatistics = {};
    controller.notificationCounters = {};

    controller.init = function () {
        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("GetMyCompanyStatistics", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (companyStatistics) {
              controller.companyStatistics = companyStatistics;
          });

        Azureservice.invokeApi("GetMyNotificationCounters", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (notificationCounters) {
              controller.notificationCounters = notificationCounters;
          });

        Azureservice.invokeApi("GetLatestProductUpdatesPaged?pageSize=" + controller.pageSize + "&pageNumber=" + controller.pageNumber, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (productUpdates) {
              controller.productUpdates = productUpdates;
          });
    };

    controller.getHeightForStatsBar = function (maxHeight, value, comparativeValue) {
        if (!value) {
            return 0;
        }

        if (value > comparativeValue) {
            return maxHeight;
        }

        return Math.floor(maxHeight * value / comparativeValue);
    }

    controller.getProductUpdateDisplayDate = function (productUpdate) {
        if (!productUpdate.DateTime) {
            return null;
        }

        var date = moment(productUpdate.DateTime);

        if (!date) {
            return null;
        }

        var now = moment();
        var difference = moment.duration(now.diff(date));
        var differenceInMinutes = difference.asMinutes();

        if (differenceInMinutes < 1) {
            return "just now";
        }

        if (differenceInMinutes < 59) {
            return Math.round(differenceInMinutes) + " min";
        }

        var differenceInHours = difference.asHours();

        if (differenceInHours < 24) {
            return Math.round(differenceInHours) + " hours";
        }

        var differenceInDays = difference.asDays();

        if (differenceInDays < 7) {
            return Math.round(differenceInDays) + " days";
        }

        return date.format("DD MMM YY");
    };

    controller.getProductUpdateUserDisplayName = function (productUpdate) {
        if (productUpdate.UserFirstName && productUpdate.UserLastName) {
            return productUpdate.UserFirstName + " " + productUpdate.UserLastName;
        }

        if (productUpdate.UserFirstName) {
            productUpdate.UserFirstName
        }

        if (productUpdate.UserLastName) {
            return productUpdate.UserLastName;
        }

        return productUpdate.UserUserName;
    };

    controller.getProductUpdateSummary = function (productUpdate) {
        switch (productUpdate.UpdateType) {
            case ProductUpdateTypes.ProductAdded: {
                return "Product added.";
            }
            case ProductUpdateTypes.ProductEdited: {
                return "Product updated.";
            }
            case ProductUpdateTypes.ProductDeleted: {
                return "Product deleted.";
            }
            case ProductUpdateTypes.ProductFileAdded: {
                return "File added.";
            }
            case ProductUpdateTypes.ProductFileEdited: {
                return "File updated.";
            }
            case ProductUpdateTypes.ProductFileDeleted: {
                return "File deleted.";
            }
            case ProductUpdateTypes.ProductCustomerAdded: {
                return "Client added.";
            }
            case ProductUpdateTypes.ProductCustomerDeleted: {
                return "Client deleted.";
            }
            default: {
                return null;
            }
        }
    };

    controller.init();
}])
.controller("myCompanyController", ["$scope", "Azureservice", "localStorageService", "$stateParams", "$state", function ($scope, Azureservice, localStorageService, $stateParams, $state) {
    var controller = this;

    controller.products = [];
    controller.company = {};

    controller.init = function () {
        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("GetMyCompany", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (company) {
              controller.company = company;
              $scope.$broadcast("dataready", null);
          });

        Azureservice.invokeApi("GetMyCompanyProducts", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (products) {
              controller.products = products;
              $scope.$broadcast("dataready", null);
          });
    };

    controller.getCompanyProfileImage = function () {
        if (!controller.company.Image) {
            return "/img/user_image.svg";
        }

        return "data:image/JPEG;base64," + controller.company.Image;
    };

    $scope.GoToEditCompany = function ()
    {
        debugger
        $state.go("companyProfileDetails");
    }

    $scope.getCompanyHeaderImage = function () {
        
        //if (!controller.company.HeaderImage) {
        //    return "/img/user_image.svg";
        //}

        return "data:image/JPEG;base64," + controller.company.HeaderImage;
    };

    controller.init();
}])
.controller("companyController", ["$scope", "Azureservice", "localStorageService", "$stateParams", function ($scope, Azureservice, localStorageService, $stateParams) {
    var controller = this;

    controller.companyId = $stateParams.companyId;
    controller.company = {};
    controller.products = [];

    controller.init = function () {
        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("GetCompany?id=" + controller.companyId, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (company) {
              controller.company = company;
          });

        Azureservice.invokeApi("GetCompanyProductSummaries?companyId=" + controller.companyId, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (products) {
              controller.products = products;
          });
    };

    controller.init();
}])
.controller("companyByUriController", ["$scope", "Azureservice", "localStorageService", "$stateParams", "$state", function ($scope, Azureservice, localStorageService, $stateParams, $state) {
    var controller = this;

    controller.companyUri = $stateParams.companyUri;
    controller.company = {};
    controller.products = [];

    controller.init = function () {
        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("GetCompanyByUri?uri=" + controller.companyUri, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (company) {
              controller.company = company;
          });

        Azureservice.invokeApi("GetCompanyProductSummariesByCompanyUri?companyUri=" + controller.companyUri, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (products) {
              controller.products = products;
          });
    };

    controller.init();
}])
.controller("myProductController", ["$scope", "Azureservice", "localStorageService", "$stateParams", "$http", function ($scope, Azureservice, localStorageService, $stateParams, $http) {
    var controller = this;

    controller.productId = $stateParams.productId;
    controller.product = {};
    controller.products = [];
    controller.productFiles = [];
    controller.productFileTriplets = [];
    controller.productCustomers = [];
    controller.followerGroups = [];
    controller.feedback = [];
    controller.feedbackVisible = false;
    controller.productFileFeedbackBody = null;
    controller.productFileFeedbackLinkUrl = null;
    controller.productFileFeedbackImage = null;
    controller.editingProductFileFeedbackLinkUrl = false;
    controller.selectedFollowerGroup = null;
    controller.flowOptions = {
        permanentErrors: [500, 501],
        maxChunkRetries: 1,
        chunkRetryInterval: 2000,
        simultaneousUploads: 4,
        progressCallbacksInterval: 500,
        generateUniqueIdentifier: function () {
            return (new Date()).getTime();
        }
    };
    controller.busy = false;
    controller.errorMessage = null;
    controller.hasError = false;
    controller.confirmationMessage = null;
    controller.hasConfirmation = false;

    controller.init = function () {
        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("GetMyCompanyProducts", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (products) {
              controller.products = products;
          });

        Azureservice.invokeApi("GetMyProduct?id=" + controller.productId, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (product) {
              controller.product = product;
          });

        Azureservice.invokeApi("GetMyProductFiles?productId=" + controller.productId + "&followerGroupId=" + (controller.selectedFollowerGroup === null ? "null" : controller.selectedFollowerGroup.ID), { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (productFiles) {
              if (productFiles.length === 0) {
                  return;
              }

              while (productFiles.length > 0) {
                  controller.productFileTriplets.push({ productFiles: productFiles.splice(0, 3) });
              }
          });

        Azureservice.invokeApi("GetMyProductCustomers?productId=" + controller.productId, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (productCustomers) {
              controller.productCustomers = productCustomers;
          });

        Azureservice.invokeApi("GetMyCompanyFollowerGroups", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (followerGroups) {
              controller.followerGroups = followerGroups;
          });
    };

    controller.toggleEditProductFileFeedbackLinkUrl = function () {
        controller.editingProductFileFeedbackLinkUrl = !controller.editingProductFileFeedbackLinkUrl;
    };

    controller.setProductFileFeedbackLinkUrl = function () {
        controller.editingProductFileFeedbackLinkUrl = false;
    };

    controller.getProductFileFeedbackImage = function () {
        if (!controller.productFileFeedbackImage) {
            return "http://placehold.it/212x212";
        }

        return "data:image/JPEG;base64," + controller.productFileFeedbackImage;
    };

    controller.flowProductFileFeedbackImageSubmitted = function ($files, $event, $flow) {
        if (!$event || !$files) {
            return;
        }

        var len = $files.length;

        if (!len) {
            return;
        }

        var reader = new FileReader();
        var base64DataBeginMarker = ";base64,";
        var base64DataBeginIndex;

        reader.addEventListener("load", function () {
            base64DataBeginIndex = reader.result.indexOf(base64DataBeginMarker);
            controller.productFileFeedbackImage = reader.result.substring(base64DataBeginIndex + base64DataBeginMarker.length);
            $flow.cancel();
            $scope.$apply();
        }, false);
        reader.readAsDataURL($files[0].file);
    };

    controller.selectFollowerGroup = function (followerGroup) {
        if (followerGroup === null && controller.selectedFollowerGroup === null) {
            return;
        }

        if (followerGroup !== null && controller.selectedFollowerGroup !== null && followerGroup.ID === controller.selectedFollowerGroup.ID) {
            return;
        }

        controller.selectedFollowerGroup = followerGroup;

        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("GetMyProductFiles?productId=" + controller.productId + "&followerGroupId=" + (controller.selectedFollowerGroup === null ? "null" : controller.selectedFollowerGroup.ID), { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (productFiles) {
              controller.productFileTriplets = [];

              if (productFiles.length === 0) {
                  return;
              }

              while (productFiles.length > 0) {
                  controller.productFileTriplets.push({ productFiles: productFiles.splice(0, 3) });
              }
          });
    };

    controller.getProductCustomerLogo = function (productCustomer) {
        if (!productCustomer.Logo) {
            return "/img/user_image.svg";
        }

        return "data:image/JPEG;base64," + productCustomer.Logo;
    };

    controller.downloadProductFile = function (productFile) {
        var authData = localStorageService.get("authorizationData");

        $http.get("/api/DownloadMyProductFile?id=" + productFile.ID, {
            headers: {
                "Authorization": "Bearer " + authData.token
            },
            responseType: "arraybuffer"
        }).success(function (fileData) {
            var file = new Blob([fileData], { type: "application/octet-stream" });

            saveAs(file, productFile.Name);
        });
    };

    controller.showProductFileFeedback = function (productFile) {
        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("GetProductFileFeedback?productFileId=" + productFile.ID, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (productFileFeedback) {
              controller.productFileFeedback = productFileFeedback;
              controller.feedbackVisible = true;
              controller.productFileSelectedForFeedback = productFile;
          });
    };

    controller.hideProductFileFeedback = function () {
        controller.feedbackVisible = false;
        controller.productFileSelectedForFeedback = null;
        controller.productFileFeedbackBody = null;
        controller.productFileFeedbackLinkUrl = null;
        controller.productFileFeedbackImage = null;
        controller.productFileFeedback = [];
    };

    controller.postProductFileFeedback = function () {
        if (!controller.productFileSelectedForFeedback || !controller.productFileFeedbackBody || controller.productFileFeedbackBody.length === 0) {
            return;
        }

        var authData = localStorageService.get("authorizationData");
        var newProductFileFeedback = {
            ProductFileID: controller.productFileSelectedForFeedback.ID,
            Title: controller.product.Name + " » " + controller.productFileSelectedForFeedback.Name,
            Body: controller.productFileFeedbackBody,
            LinkUrl: controller.productFileFeedbackLinkUrl,
            Image: controller.productFileFeedbackImage
        };

        controller.busy = true;
        Azureservice.invokeApi("NewProductFileFeedback", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: newProductFileFeedback })
            .then(function (productFileFeedback) {
                controller.productFileFeedback.push(productFileFeedback);
                controller.productFileFeedbackBody = null;
                controller.productFileFeedbackLinkUrl = null;
                controller.productFileFeedbackImage = null;
            })
            .finally(function () {
                controller.busy = false;
            });
    };

    controller.getArticleUserDisplayName = function (article) {
        if (article.UserFirstName && article.UserLastName) {
            return article.UserFirstName + " " + article.UserLastName;
        }

        if (article.UserFirstName) {
            return article.UserFirstName
        }

        if (article.UserLastName) {
            return article.UserLastName;
        }

        return article.UserUserName;
    };

    controller.getFeedbackDisplayDate = function (feedback) {
        if (!feedback.PostedAt) {
            return null;
        }

        var date = moment(feedback.PostedAt);

        if (!date) {
            return null;
        }

        var now = moment();
        var difference = moment.duration(now.diff(date));
        var differenceInMinutes = difference.asMinutes();

        if (differenceInMinutes < 1) {
            return "(just now)";
        }

        if (differenceInMinutes < 59) {
            return "(" + Math.round(differenceInMinutes) + " minutes ago)";
        }

        var differenceInHours = difference.asHours();

        if (differenceInHours < 24) {
            return "(" + Math.round(differenceInHours) + " hours ago)";
        }

        var differenceInDays = difference.asDays();

        if (differenceInDays < 7) {
            return "(" + Math.round(differenceInDays) + " days ago)";
        }

        return "(" + date.format("DD MMM YY") + ")";
    };

    controller.init();
}])
.controller("productController", ["$scope", "Azureservice", "localStorageService", "$stateParams", "$http", function ($scope, Azureservice, localStorageService, $stateParams, $http) {
    var controller = this;

    controller.productId = $stateParams.productId;
    controller.companyId = $stateParams.companyId;
    controller.product = {};
    controller.products = [];
    controller.productFiles = [];
    controller.productFileTriplets = [];
    controller.productCustomers = [];
    controller.feedback = [];
    controller.feedbackVisible = false;
    controller.productFileFeedbackBody = null;
    controller.productFileFeedbackLinkUrl = null;
    controller.productFileFeedbackImage = null;
    controller.editingProductFileFeedbackLinkUrl = false;
    controller.selectedFollowerGroup = null;
    controller.flowOptions = {
        permanentErrors: [500, 501],
        maxChunkRetries: 1,
        chunkRetryInterval: 2000,
        simultaneousUploads: 4,
        progressCallbacksInterval: 500,
        generateUniqueIdentifier: function () {
            return (new Date()).getTime();
        }
    };
    controller.busy = false;
    controller.errorMessage = null;
    controller.hasError = false;
    controller.confirmationMessage = null;
    controller.hasConfirmation = false;

    controller.init = function () {
        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("GetCompanyProducts?companyId=" + controller.companyId, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (products) {
              controller.products = products;
          });

        Azureservice.invokeApi("GetProduct?id=" + controller.productId, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (product) {
              controller.product = product;
          });

        Azureservice.invokeApi("GetProductFiles?productId=" + controller.productId, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (productFiles) {
              if (productFiles.length === 0) {
                  return;
              }

              while (productFiles.length > 0) {
                  controller.productFileTriplets.push({ productFiles: productFiles.splice(0, 3) });
              }
          });

        Azureservice.invokeApi("GetProductCustomers?productId=" + controller.productId, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (productCustomers) {
              controller.productCustomers = productCustomers;
          });
    };

    controller.toggleEditProductFileFeedbackLinkUrl = function () {
        controller.editingProductFileFeedbackLinkUrl = !controller.editingProductFileFeedbackLinkUrl;
    };

    controller.setProductFileFeedbackLinkUrl = function () {
        controller.editingProductFileFeedbackLinkUrl = false;
    };

    controller.getProductFileFeedbackImage = function () {
        if (!controller.productFileFeedbackImage) {
            return "http://placehold.it/212x212";
        }

        return "data:image/JPEG;base64," + controller.productFileFeedbackImage;
    };

    controller.flowProductFileFeedbackImageSubmitted = function ($files, $event, $flow) {
        if (!$event || !$files) {
            return;
        }

        var len = $files.length;

        if (!len) {
            return;
        }

        var reader = new FileReader();
        var base64DataBeginMarker = ";base64,";
        var base64DataBeginIndex;

        reader.addEventListener("load", function () {
            base64DataBeginIndex = reader.result.indexOf(base64DataBeginMarker);
            controller.productFileFeedbackImage = reader.result.substring(base64DataBeginIndex + base64DataBeginMarker.length);
            $flow.cancel();
            $scope.$apply();
        }, false);
        reader.readAsDataURL($files[0].file);
    };

    controller.getProductCustomerLogo = function (productCustomer) {
        if (!productCustomer.Logo) {
            return "/img/user_image.svg";
        }

        return "data:image/JPEG;base64," + productCustomer.Logo;
    };

    controller.downloadProductFile = function (productFile) {
        var authData = localStorageService.get("authorizationData");

        $http.get("/api/DownloadProductFile?id=" + productFile.ID, {
            headers: {
                "Authorization": "Bearer " + authData.token
            },
            responseType: "arraybuffer"
        }).success(function (fileData) {
            var file = new Blob([fileData], { type: "application/octet-stream" });

            saveAs(file, productFile.Name);
        });
    };

    controller.showProductFileFeedback = function (productFile) {
        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("GetProductFileFeedback?productFileId=" + productFile.ID, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (productFileFeedback) {
              controller.productFileFeedback = productFileFeedback;
              controller.feedbackVisible = true;
              controller.productFileSelectedForFeedback = productFile;
          });
    };

    controller.hideProductFileFeedback = function () {
        controller.feedbackVisible = false;
        controller.productFileSelectedForFeedback = null;
        controller.productFileFeedbackBody = null;
        controller.productFileFeedbackLinkUrl = null;
        controller.productFileFeedbackImage = null;
        controller.productFileFeedback = [];
    };

    controller.postProductFileFeedback = function () {
        if (!controller.productFileSelectedForFeedback || !controller.productFileFeedbackBody || controller.productFileFeedbackBody.length === 0) {
            return;
        }

        var authData = localStorageService.get("authorizationData");
        var newProductFileFeedback = {
            ProductFileID: controller.productFileSelectedForFeedback.ID,
            Title: controller.product.Name + " » " + controller.productFileSelectedForFeedback.Name,
            Body: controller.productFileFeedbackBody,
            LinkUrl: controller.productFileFeedbackLinkUrl,
            Image: controller.productFileFeedbackImage
        };

        controller.busy = true;
        Azureservice.invokeApi("NewProductFileFeedback", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: newProductFileFeedback })
            .then(function (productFileFeedback) {
                controller.productFileFeedback.push(productFileFeedback);
                controller.productFileFeedbackBody = null;
                controller.productFileFeedbackLinkUrl = null;
                controller.productFileFeedbackImage = null;
            })
            .finally(function () {
                controller.busy = false;
            });
    };

    controller.getArticleUserDisplayName = function (article) {
        if (article.UserFirstName && article.UserLastName) {
            return article.UserFirstName + " " + article.UserLastName;
        }

        if (article.UserFirstName) {
            return article.UserFirstName
        }

        if (article.UserLastName) {
            return article.UserLastName;
        }

        return article.UserUserName;
    };

    controller.getFeedbackDisplayDate = function (feedback) {
        if (!feedback.PostedAt) {
            return null;
        }

        var date = moment(feedback.PostedAt);

        if (!date) {
            return null;
        }

        var now = moment();
        var difference = moment.duration(now.diff(date));
        var differenceInMinutes = difference.asMinutes();

        if (differenceInMinutes < 1) {
            return "(just now)";
        }

        if (differenceInMinutes < 59) {
            return "(" + Math.round(differenceInMinutes) + " minutes ago)";
        }

        var differenceInHours = difference.asHours();

        if (differenceInHours < 24) {
            return "(" + Math.round(differenceInHours) + " hours ago)";
        }

        var differenceInDays = difference.asDays();

        if (differenceInDays < 7) {
            return "(" + Math.round(differenceInDays) + " days ago)";
        }

        return "(" + date.format("DD MMM YY") + ")";
    };

    controller.init();
}])
.controller("productByUriController", ["$scope", "Azureservice", "localStorageService", "$stateParams", "$http", function ($scope, Azureservice, localStorageService, $stateParams, $http) {
    var controller = this;

    controller.productUri = $stateParams.productUri;
    controller.companyUri = $stateParams.companyUri;
    controller.productId = null;
    controller.companyId = null;
    controller.product = {};
    controller.products = [];
    controller.productFiles = [];
    controller.productFileTriplets = [];
    controller.productCustomers = [];
    controller.feedback = [];
    controller.feedbackVisible = false;
    controller.productFileFeedbackBody = null;
    controller.productFileFeedbackLinkUrl = null;
    controller.productFileFeedbackImage = null;
    controller.editingProductFileFeedbackLinkUrl = false;
    controller.selectedFollowerGroup = null;
    controller.flowOptions = {
        permanentErrors: [500, 501],
        maxChunkRetries: 1,
        chunkRetryInterval: 2000,
        simultaneousUploads: 4,
        progressCallbacksInterval: 500,
        generateUniqueIdentifier: function () {
            return (new Date()).getTime();
        }
    };
    controller.busy = false;
    controller.errorMessage = null;
    controller.hasError = false;
    controller.confirmationMessage = null;
    controller.hasConfirmation = false;

    controller.init = function () {
        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("GetProductByUri?companyUri=" + controller.companyUri + "&productUri=" + controller.productUri, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (product) {
              controller.product = product;
              controller.productId = product.ID;
              controller.companyId = product.CompanyID;

              Azureservice.invokeApi("GetCompanyProducts?companyId=" + controller.companyId, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
                .then(function (products) {
                    controller.products = products;
                });

              Azureservice.invokeApi("GetProductFiles?productId=" + controller.productId, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
                .then(function (productFiles) {
                    if (productFiles.length === 0) {
                        return;
                    }

                    while (productFiles.length > 0) {
                        controller.productFileTriplets.push({ productFiles: productFiles.splice(0, 3) });
                    }
                });

              Azureservice.invokeApi("GetProductCustomers?productId=" + controller.productId, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
                .then(function (productCustomers) {
                    controller.productCustomers = productCustomers;
                });
          });
    };

    controller.toggleEditProductFileFeedbackLinkUrl = function () {
        controller.editingProductFileFeedbackLinkUrl = !controller.editingProductFileFeedbackLinkUrl;
    };

    controller.setProductFileFeedbackLinkUrl = function () {
        controller.editingProductFileFeedbackLinkUrl = false;
    };

    controller.getProductFileFeedbackImage = function () {
        if (!controller.productFileFeedbackImage) {
            return "http://placehold.it/212x212";
        }

        return "data:image/JPEG;base64," + controller.productFileFeedbackImage;
    };

    controller.flowProductFileFeedbackImageSubmitted = function ($files, $event, $flow) {
        if (!$event || !$files) {
            return;
        }

        var len = $files.length;

        if (!len) {
            return;
        }

        var reader = new FileReader();
        var base64DataBeginMarker = ";base64,";
        var base64DataBeginIndex;

        reader.addEventListener("load", function () {
            base64DataBeginIndex = reader.result.indexOf(base64DataBeginMarker);
            controller.productFileFeedbackImage = reader.result.substring(base64DataBeginIndex + base64DataBeginMarker.length);
            $flow.cancel();
            $scope.$apply();
        }, false);
        reader.readAsDataURL($files[0].file);
    };

    controller.getProductCustomerLogo = function (productCustomer) {
        if (!productCustomer.Logo) {
            return "/img/user_image.svg";
        }

        return "data:image/JPEG;base64," + productCustomer.Logo;
    };

    controller.downloadProductFile = function (productFile) {
        var authData = localStorageService.get("authorizationData");

        $http.get("/api/DownloadProductFile?id=" + productFile.ID, {
            headers: {
                "Authorization": "Bearer " + authData.token
            },
            responseType: "arraybuffer"
        }).success(function (fileData) {
            var file = new Blob([fileData], { type: "application/octet-stream" });

            saveAs(file, productFile.Name);
        });
    };

    controller.showProductFileFeedback = function (productFile) {
        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("GetProductFileFeedback?productFileId=" + productFile.ID, { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (productFileFeedback) {
              controller.productFileFeedback = productFileFeedback;
              controller.feedbackVisible = true;
              controller.productFileSelectedForFeedback = productFile;
          });
    };

    controller.hideProductFileFeedback = function () {
        controller.feedbackVisible = false;
        controller.productFileSelectedForFeedback = null;
        controller.productFileFeedbackBody = null;
        controller.productFileFeedbackLinkUrl = null;
        controller.productFileFeedbackImage = null;
        controller.productFileFeedback = [];
    };

    controller.postProductFileFeedback = function () {
        if (!controller.productFileSelectedForFeedback || !controller.productFileFeedbackBody || controller.productFileFeedbackBody.length === 0) {
            return;
        }

        var authData = localStorageService.get("authorizationData");
        var newProductFileFeedback = {
            ProductFileID: controller.productFileSelectedForFeedback.ID,
            Title: controller.product.Name + " » " + controller.productFileSelectedForFeedback.Name,
            Body: controller.productFileFeedbackBody,
            LinkUrl: controller.productFileFeedbackLinkUrl,
            Image: controller.productFileFeedbackImage
        };

        controller.busy = true;
        Azureservice.invokeApi("NewProductFileFeedback", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: newProductFileFeedback })
            .then(function (productFileFeedback) {
                controller.productFileFeedback.push(productFileFeedback);
                controller.productFileFeedbackBody = null;
                controller.productFileFeedbackLinkUrl = null;
                controller.productFileFeedbackImage = null;
            })
            .finally(function () {
                controller.busy = false;
            });
    };

    controller.getArticleUserDisplayName = function (article) {
        if (article.UserFirstName && article.UserLastName) {
            return article.UserFirstName + " " + article.UserLastName;
        }

        if (article.UserFirstName) {
            return article.UserFirstName
        }

        if (article.UserLastName) {
            return article.UserLastName;
        }

        return article.UserUserName;
    };

    controller.getFeedbackDisplayDate = function (feedback) {
        if (!feedback.PostedAt) {
            return null;
        }

        var date = moment(feedback.PostedAt);

        if (!date) {
            return null;
        }

        var now = moment();
        var difference = moment.duration(now.diff(date));
        var differenceInMinutes = difference.asMinutes();

        if (differenceInMinutes < 1) {
            return "(just now)";
        }

        if (differenceInMinutes < 59) {
            return "(" + Math.round(differenceInMinutes) + " minutes ago)";
        }

        var differenceInHours = difference.asHours();

        if (differenceInHours < 24) {
            return "(" + Math.round(differenceInHours) + " hours ago)";
        }

        var differenceInDays = difference.asDays();

        if (differenceInDays < 7) {
            return "(" + Math.round(differenceInDays) + " days ago)";
        }

        return "(" + date.format("DD MMM YY") + ")";
    };

    controller.init();
}])
.controller("onboardingController", ["$scope", "$state", "Azureservice", "localStorageService", function ($scope, $state, Azureservice, localStorageService) {
    var controller = this;
    controller.statusDto = {};

    controller.init = function () {
        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("GetMyOnboardingStatus", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (onboardingStatus) {
              if (onboardingStatus.OnboardingSkipped) {
                  $state.go("dashboard");

                  return;
              }

              if (!onboardingStatus.OnboardingStep) {
                  return;
              }

              if (onboardingStatus.OnboardingStep === 1) {
                  $state.go("onboardingStepOne");

                  return;
              }

              if (onboardingStatus.OnboardingStep === 2) {
                  $state.go("onboardingStepTwo");

                  return;
              }

              if (onboardingStatus.OnboardingStep === 3) {
                  $state.go("onboardingStepThree");

                  return;
              }

              if (onboardingStatus.OnboardingStep === 4) {
                  $state.go("onboardingStepFour");

                  return;
              }
          });
    };

    controller.startOnboarding = function () {
        var authData = localStorageService.get("authorizationData");
        controller.statusDto.OnboardingStep = 1;
        Azureservice.invokeApi("SetMyCurrentOnboardingStep", { method: "POST", body: controller.statusDto, headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (onboardingStatus) {
              $state.go("onboardingStepOne");
          });
    };

    controller.skipOnboarding = function () {
        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("SkipOnboarding", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (onboardingStatus) {
              $state.go("dashboard");
          });
    };

    controller.init();
}])
.controller("onboardingStepOneController", ["$scope", "Azureservice", "localStorageService", "$timeout", "$rootScope", "$state", "SectorService", "Countries", "TransferData", function ($scope, Azureservice, localStorageService, $timeout, $rootScope, $state, SectorService, Countries, TransferData, InviteExistingUser) {
    var controller = this;
    $scope.user = {};
    $scope.options = ['One', 'Two'];
    controller.company = {};
    controller.companySelectionStatus = {};
    controller.statusDto = {};
    controller.countries = Countries;
    $scope.searchQuery;


    controller.flowOptions = {
        target: "/api/CompanyProfileImageUpload",
        permanentErrors: [500, 501],
        maxChunkRetries: 1,
        chunkRetryInterval: 2000,
        simultaneousUploads: 4,
        progressCallbacksInterval: 500,
        headers: function () {
            var authData = localStorageService.get("authorizationData");

            return { "Authorization": "Bearer " + authData.token };
        },
        generateUniqueIdentifier: function () {
            return (new Date()).getTime();
        }
    };
    controller.hasImagesToUpload = false;
    controller.ImageType = null;
    controller.validationErrors = [];
    controller.companySectors = {};
    //controller.companySectors = CompanySectors;
    controller.busy = false;
    controller.errorMessage = null;
    controller.hasError = false;
    controller.confirmationMessage = null;
    controller.hasConfirmation = false;
    $scope._selectedSector = ""
    $scope._selectedCoreService = "";
    $scope.skillList = {};
    $scope.dataHasLoaded = false;

    controller.init = function () {
        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("GetSectors", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
            .then(function (sectors) {
                controller.companySectors = sectors;
            });

        Azureservice.invokeApi("GetSkills", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
            .then(function (skills) {
                $scope.skillList = skills;
                //$rootScope.CoreServices = skills;
                $scope.dataHasLoaded = true;
                TransferData.AddSkillsData($scope.skillList);
            });

             

        Azureservice.invokeApi("GetMyOnboardingStatus", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (onboardingStatus) {
              if (onboardingStatus.OnboardingSkipped) {
                  $state.go("dashboard");
                  return;
              }

              if (!onboardingStatus.OnboardingStep) {
                  return;
              }

              if (onboardingStatus.OnboardingStep === 1) {
                  if (onboardingStatus.IsIndividual == true) {
                      controller.companySelectionStatus.CompanySelectionType = 3;
                  }
                  else {
                      controller.companySelectionStatus.CompanySelectionType = 2;
                  }
              }

              if (onboardingStatus.OnboardingStep === 2) {
                  $state.go("onboardingStepTwo");

                  return;
              }

              if (onboardingStatus.OnboardingStep === 3) {
                  $state.go("onboardingStepThree");
                  return;
              }

              if (onboardingStatus.OnboardingStep === 4) {
                  $state.go("onboardingStepFour");
                  return;
              }

              Azureservice.invokeApi("GetMyCompanyProfile", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
                .then(function (company) {
                    controller.company = company;
                    $scope._selectedSector = controller.company.Sector;
                    $scope.tags = controller.company.Skills;

                  });

              Azureservice.invokeApi("GetMyPersonalProfile", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
                  .then(function (user) {
                      $scope.user = user;
                      $scope.Individual = user.IsIndividual;
                      $scope.$broadcast("dataready", null);
                  });
          });
    };

    controller.setCompanySector = function (sector) {
        controller.company.Sector = sector.SectorName;
    };
    $scope.AddSectorEnable = function () {
        $scope.AddSectorEnabled = true;
    }

    $scope.AddSector = function () {
        $scope.SectorError = false;
        if ($scope.SectorName == undefined || $scope.SectorName == "") {
            $scope.SectorError = true;
        }
        else {
            $scope.AddSectorEnabled = false;
            var sectorAdded = SectorService.AddNewSector($scope.SectorName);
            sectorAdded.then(function (Response) {
                var GetData = SectorService.GetSector();
                GetData.then(function (Response) {
                    controller.companySectors = Response.data;
                    $scope.SectorName = "";
                })
            })
        }
    };
    $scope.CloseSector = function () {
        $scope.AddSectorEnabled = false;
        $scope.SectorError = false;
    }

    controller.skipOnboarding = function () {
        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("SkipOnboarding", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (onboardingStatus) {
              $state.go("dashboard");
          });
    };

    controller.nextStep = function ($flow) {
        var authData = localStorageService.get("authorizationData");
        controller.resetNotifications();
        // controller.busy = true;

        if (controller.companySelectionStatus.CompanySelectionType == 2) {
            controller.company.Skills = $scope.tags;
            controller.company.Sector = $scope._selectedSector;
            Azureservice.invokeApi("UpdateMyCompanyProfile", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: controller.company })

                .then(function (company) {
                    controller.company = company;
                    controller.showConfirmation("Your company profile was saved");

                    if (controller.hasImagesToUpload !== true) {
                        $timeout(function () {
                            $rootScope.$emit("CompanyProfileImageChanged");
                        }, 1000);
                    }

                    controller.hasImagesToUpload = false;
                    controller.statusDto.OnboardingStep = 2;
                    controller.statusDto.IsIndividual = false;

                    Azureservice.invokeApi("SetMyCurrentOnboardingStep", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: controller.statusDto })
                        .then(function (onboardingStatus) {
                            $state.go("onboardingStepTwo");
                        });
                })
                .finally(function () {
                    controller.busy = false;
                });
        }
        else if (controller.companySelectionStatus.CompanySelectionType == 1) {
            controller.statusDto.OnboardingStep = 2;
            controller.statusDto.IsIndividual = false;
            
            Azureservice.invokeApi("LinkCompanyMember", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: $scope.CompanyModel.ID })
               .then(function (onboardingStatus) {
               });


            Azureservice.invokeApi("SetMyCurrentOnboardingStep", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: controller.statusDto })
                .then(function (onboardingStatus) {
                    $state.go("onboardingStepTwo");
                });
        }
        else if (controller.companySelectionStatus.CompanySelectionType == 3) {
            
            controller.statusDto.OnboardingStep = 4;
            controller.statusDto.IsIndividual = true;
            Azureservice.invokeApi("UpdateMyPersonalProfile", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: $scope.user })
            .then(function (user) {
                controller.user = user;
                controller.showConfirmation("Your personal details were saved");

                if (controller.hasImagesToUpload !== true) {
                    $timeout(function () {
                        $rootScope.$emit("UserProfileImageChanged");
                    }, 1000);
                }

                controller.hasImagesToUpload = false;

                Azureservice.invokeApi("SetMyCurrentOnboardingStep", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: controller.statusDto })
               .then(function (onboardingStatus) {
                   $state.go("onboardingStepFour");
               });

            })
            .finally(function () {
                controller.busy = false;
            });

           
        }
    };

    controller.getCompanyProfileImage = function () {
        if (!controller.company.Image) {
            return "/img/user_image.svg";
        }

        return "data:image/JPEG;base64," + controller.company.Image;
    };



    controller.flowFilesSubmitted = function ($files, $event, $flow) {
        if (!$event) {
            return;
        }

        var i;
        var reader = new FileReader();
        var base64DataBeginMarker = ";base64,";
        var base64DataBeginIndex;

        reader.addEventListener("load", function () {
            base64DataBeginIndex = reader.result.indexOf(base64DataBeginMarker);
            controller.company.Image = reader.result.substring(base64DataBeginIndex + base64DataBeginMarker.length);
            controller.ImageType = $files[0].getType();
            $flow.cancel();
            controller.hasImagesToUpload = true;
            $scope.$apply();
        }, false);
        reader.readAsDataURL($files[0].file);
    };

    controller.base64StringToBlob = function (base64String, contentType, sliceSize) {
        contentType = contentType || "";
        sliceSize = sliceSize || 512;

        var byteCharacters = atob(base64String);
        var byteArrays = [];
        var slice;
        var byteNumbers;
        var byteArray;
        var blob;

        for (var offset = 0; offset < byteCharacters.length; offset += sliceSize) {
            slice = byteCharacters.slice(offset, offset + sliceSize);
            byteNumbers = new Array(slice.length);

            for (var i = 0; i < slice.length; i++) {
                byteNumbers[i] = slice.charCodeAt(i);
            }

            byteArray = new Uint8Array(byteNumbers);
            byteArrays.push(byteArray);
        }

        blob = new Blob(byteArrays, { type: contentType });

        return blob;
    };
    controller.showError = function (errorMessage) {
        controller.errorMessage = errorMessage;
        controller.hasError = true;

        $timeout(function () {
            controller.errorMessage = null;
            controller.hasError = false;
        }, 15000);
        $("html, body").animate({
            scrollTop: 0
        }, 500);
    };

    controller.showConfirmation = function (confirmationMessage) {
        controller.confirmationMessage = confirmationMessage;
        controller.hasConfirmation = true;

        $timeout(function () {
            controller.confirmationMessage = null;
            controller.hasConfirmation = false;
        }, 15000);
        $("html, body").animate({
            scrollTop: 0
        }, 500);
    };

    controller.resetNotifications = function () {
        controller.errorMessage = null;
        controller.hasError = false;
        controller.confirmationMessage = null;
        controller.hasConfirmation = false;
    };

    controller.search = function (searchQuery) {
        console.log("searching for : " + searchQuery)
        var authData = localStorageService.get("authorizationData");
        return Azureservice.invokeApi("SearchCompanies?query=" + encodeURIComponent(searchQuery) + "&maxRecords=10", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } });
    };

    controller.showProducts = function () {
        controller.companiesVisible = false;
    };

    controller.showCompanies = function () {
        controller.companiesVisible = true;
    };

    //$scope.onSelect = function ($item, $model, $label) {
    //    console.log("this is selected..");
    //    $scope.searchQueryResult = $model.DisplayName;
    //    $scope.searchQuery = $model.DisplayName;
    //    $scope.selectedQueryCompanyId = $model.ID;
    //};
     
    // $scope.onSelect = function ($model) {
    // 
    //    console.log("this is selected..");
    //   // $scope.CompanyModel = $model.DisplayName;
    //    //$scope.searchQuery = $model.DisplayName;
    //    //$scope.selectedQueryCompanyId = $model.ID;
    //};

    $scope.ngSectorOptionSelected = function (value) {
        if (arguments.length) {
            $scope._selectedSector = value;
        } else {
            return $scope._selectedSector;
        }
    };
    $scope.modelOptions = {
        debounce: {
            default: 500,
            blur: 250
        },
        getterSetter: true
    };

    $scope.ngCoreServicesSelected = function (value) {
        
        //if (arguments.length) {
        //    if (value != "" && value != undefined) {
        //        $scope._selectedCoreService = value;
        //    }
        //} else {
        //    return $scope._selectedCoreService;
        //}
    };

    $scope.modelOptionsSkills = {

        debounce: {

            default: 500,
            blur: 250
        },

        getterSetter: true
    };

    //$scope.onSelectSkills = function ($item, $model, $label, $event) {
    //    
    //    if ($scope.tags.indexOf($item.SkillName) == -1) {
    //        $scope.tags.push($item.SkillName);
    //        $scope._selectedCoreService = "";
    //    }
    //    else {
    //        $scope._selectedCoreService = "";
    //    }
    //};


    $scope.onSelectCompany = function (id) {
        
       // console.log($item);
    };

    $scope.filterValue = function ($event) {
        if (isNaN(String.fromCharCode($event.keyCode))) {
            $event.preventDefault();
        }
    };

    $scope.tags = [];

    //$scope.GetCompanyData = function(name)
    //{
                
    //}

    $scope.IndividualUserDetails = function (UserDetails) {
        $rootScope.$emit("CallParentMethod", UserDetails);
    }

    //$scope.getUserProfileImage = function () {
    //    
        
    //        return "/img/user_image.svg";
        

    //  };

    controller.getUserProfileImage = function () {
      
        if (!$scope.user.Image) {
            return "/img/user_image.svg";
        }

        return "data:image/JPEG;base64," + $scope.user.Image;
    };
    
    $scope.setUserCountry = function (country) {
        $scope.user.Country = country;
    }

    controller.flowFilesSubmitted = function ($files, $event, $flow) {
        if (!$event) {
            return;
        }

        var i;
        var reader = new FileReader();
        var base64DataBeginMarker = ";base64,";
        var base64DataBeginIndex;

        reader.addEventListener("load", function () {
            base64DataBeginIndex = reader.result.indexOf(base64DataBeginMarker);
            $scope.user.Image = reader.result.substring(base64DataBeginIndex + base64DataBeginMarker.length);
            controller.ImageType = $files[0].getType();
            $flow.cancel();
            controller.hasImagesToUpload = true;
            $scope.$apply();
        }, false);
        reader.readAsDataURL($files[0].file);
    };
    controller.init();


}])
.controller("onboardingStepTwoController", ["$scope", "Azureservice", "localStorageService", "$timeout", "$rootScope", "$state", function ($scope, Azureservice, localStorageService, $timeout, $rootScope, $state) {
    var controller = this;
    controller.statusDto = {};
    controller.productId = null;
    controller.flowOptions = {
        target: "/api/ProductLogoUpload",
        permanentErrors: [500, 501],
        maxChunkRetries: 1,
        chunkRetryInterval: 2000,
        simultaneousUploads: 4,
        progressCallbacksInterval: 500,
        headers: function () {
            var authData = localStorageService.get("authorizationData");

            return { "Authorization": "Bearer " + authData.token };
        },
        generateUniqueIdentifier: function () {
            return (new Date()).getTime();
        }
    };
    controller.products = [];
    controller.followerGroups = [];
    controller.product = {};
    controller.productCustomers = [];
    controller.busy = false;
    controller.busyBack = false;
    controller.productFileIndex++;
    controller.errorMessage = null;
    controller.hasError = false;
    controller.confirmationMessage = null;
    controller.hasConfirmation = false;

    controller.init = function () {
        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("GetMyOnboardingStatus", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (onboardingStatus) {


              if (onboardingStatus.OnboardingSkipped) {
                  $state.go("dashboard");

                  return;
              }

              if (!onboardingStatus.OnboardingStep) {
                  return;
              }

              if (onboardingStatus.OnboardingStep === 1) {
                  $state.go("onboardingStepOne");

                  return;
              }

              if (onboardingStatus.OnboardingStep === 3) {
                  $state.go("onboardingStepThree");

                  return;
              }

              if (onboardingStatus.OnboardingStep === 4) {
                  $state.go("onboardingStepFour");

                  return;
              }
              Azureservice.invokeApi("GetMyCompanyProducts", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
                .then(function (products) {
                    
                    controller.product = products[0];
                });
          });
    };

    controller.skipOnboarding = function () {
        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("SkipOnboarding", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (onboardingStatus) {
              $state.go("dashboard");
          });
    };

    controller.updateProductDetails = function () {
        if (!controller.productId) {
            return controller.addNewProduct();
        }

        var authData = localStorageService.get("authorizationData");

        controller.busy = true;
        controller.resetNotifications();
        Azureservice.invokeApi("UpdateProduct", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: controller.product })
            .then(function (product) {
                controller.product = product;
                controller.showConfirmation("Your changes were saved");
                Azureservice.invokeApi("GetMyCompanyProducts", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
                  .then(function (products) {
                      controller.products = products;
                  });
            })
            .finally(function () {
                controller.busy = false;
            });
    };

    controller.nextStep = function () {
        var authData = localStorageService.get("authorizationData");
        
        controller.busy = true;
        controller.resetNotifications();
        
        if (!controller.product.ID) {
            
            Azureservice.invokeApi("NewProduct", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: controller.product })
                .then(function (product) {
                    controller.product = product;
                    controller.productId = product.ID;
                    controller.showConfirmation("Your changes were saved");

                    controller.statusDto.OnboardingStep = 3;
                    Azureservice.invokeApi("SetMyCurrentOnboardingStep", { method: "POST", body: controller.statusDto, headers: { "Authorization": "Bearer " + authData.token } })
                      .then(function (onboardingStatus) {
                          $state.go("onboardingStepThree");
                      });
                })
                .finally(function () {
                    controller.busy = false;
                });
        }
        else {
            Azureservice.invokeApi("UpdateProduct", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: controller.product })
                .then(function (product) {
                    controller.product = product;
                    controller.showConfirmation("Your changes were saved");
                    controller.statusDto.OnboardingStep = 3;
                    Azureservice.invokeApi("SetMyCurrentOnboardingStep", { method: "POST", body: controller.statusDto, headers: { "Authorization": "Bearer " + authData.token } })
                      .then(function (onboardingStatus) {
                          $state.go("onboardingStepThree");
                      });
                })
                .finally(function () {
                    controller.busy = false;
                });
        }
    };

    controller.prevStep = function () {
        var authData = localStorageService.get("authorizationData");
        controller.busyBack = true;
        controller.resetNotifications();
        controller.statusDto.OnboardingStep = 1;
        Azureservice.invokeApi("SetMyCurrentOnboardingStep", { method: "POST", body: controller.statusDto, headers: { "Authorization": "Bearer " + authData.token } })
            .then(function (onboardingStatus) {
                $state.go("onboardingStepOne");
            })
            .finally(function () {
                controller.busy = false;
            });
    };

    controller.addNewProductCustomer = function (productCustomer) {
        var authData = localStorageService.get("authorizationData");

        controller.busy = true;
        controller.resetNotifications();
        Azureservice.invokeApi("NewCustomer", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: productCustomer })
            .then(function (productCustomer) {
                controller.showConfirmation("Added new customer logo");
                productCustomer.Editing = false;
                controller.productCustomers.push(productCustomer);
            })
            .finally(function () {
                controller.busy = false;
            });
    };

    controller.getProductLogo = function () {
        if (!controller.product.Logo) {
            return "http://placehold.it/212x212";
        }

        return "data:image/JPEG;base64," + controller.product.Logo;
    };

    controller.getProductCustomerLogo = function (productCustomer) {
        if (!productCustomer.Logo) {
            return "http://placehold.it/212x212";
        }

        return "data:image/JPEG;base64," + productCustomer.Logo;
    };

    controller.flowProductCustomerLogoImagesSubmitted = function ($files, $event, $flow) {
        if (!$event || !$files) {
            return;
        }

        var len = $files.length;

        if (!len) {
            return;
        }

        if (!controller.productId) {
            var authData = localStorageService.get("authorizationData");

            controller.busy = true;
            controller.resetNotifications();
            Azureservice.invokeApi("NewProduct", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: controller.product })
                .then(function (product) {
                    controller.product = product;
                    controller.productId = product.ID;

                    for (var i = 0; i < len; i++) {
                        var reader = new FileReader();

                        reader.addEventListener("load", function (reader) {
                            var base64DataBeginMarker = ";base64,";
                            var base64DataBeginIndex = reader.result.indexOf(base64DataBeginMarker);
                            var productCustomer = {
                                ProductID: controller.productId,
                                Logo: reader.result.substring(base64DataBeginIndex + base64DataBeginMarker.length)
                            };

                            controller.addNewProductCustomer(productCustomer);
                            $flow.cancel();
                            $scope.$apply();
                        }.bind(null, reader), false);
                        reader.readAsDataURL($files[i].file);
                    }
                })
                .finally(function () {
                    controller.busy = false;
                });
        }
        else {
            for (var i = 0; i < len; i++) {
                var reader = new FileReader();

                reader.addEventListener("load", function (reader) {
                    var base64DataBeginMarker = ";base64,";
                    var base64DataBeginIndex = reader.result.indexOf(base64DataBeginMarker);
                    var productCustomer = {
                        ProductID: controller.productId,
                        Logo: reader.result.substring(base64DataBeginIndex + base64DataBeginMarker.length)
                    };

                    controller.addNewProductCustomer(productCustomer);
                    $flow.cancel();
                    $scope.$apply();
                }.bind(null, reader), false);
                reader.readAsDataURL($files[i].file);
            }
        }
    };

    controller.flowProductLogoImageSubmitted = function ($files, $event, $flow) {
        if (!$event || !$files) {
            return;
        }

        var len = $files.length;

        if (!len) {
            return;
        }

        var reader = new FileReader();
        var base64DataBeginMarker = ";base64,";
        var base64DataBeginIndex;

        reader.addEventListener("load", function () {
            base64DataBeginIndex = reader.result.indexOf(base64DataBeginMarker);
            controller.product.Logo = reader.result.substring(base64DataBeginIndex + base64DataBeginMarker.length);
            $flow.cancel();
            $scope.$apply();
        }, false);
        reader.readAsDataURL($files[0].file);
    };

    controller.base64StringToBlob = function (base64String, contentType, sliceSize) {
        contentType = contentType || "";
        sliceSize = sliceSize || 512;

        var byteCharacters = atob(base64String);
        var byteArrays = [];
        var slice;
        var byteNumbers;
        var byteArray;
        var blob;

        for (var offset = 0; offset < byteCharacters.length; offset += sliceSize) {
            slice = byteCharacters.slice(offset, offset + sliceSize);
            byteNumbers = new Array(slice.length);

            for (var i = 0; i < slice.length; i++) {
                byteNumbers[i] = slice.charCodeAt(i);
            }

            byteArray = new Uint8Array(byteNumbers);
            byteArrays.push(byteArray);
        }

        blob = new Blob(byteArrays, { type: contentType });

        return blob;
    };

    controller.showError = function (errorMessage) {
        controller.errorMessage = errorMessage;
        controller.hasError = true;

        $timeout(function () {
            controller.errorMessage = null;
            controller.hasError = false;
        }, 15000);
        $("html, body").animate({
            scrollTop: 0
        }, 500);
    };

    controller.showConfirmation = function (confirmationMessage) {
        controller.confirmationMessage = confirmationMessage;
        controller.hasConfirmation = true;

        $timeout(function () {
            controller.confirmationMessage = null;
            controller.hasConfirmation = false;
        }, 15000);
        $("html, body").animate({
            scrollTop: 0
        }, 500);
    };

    controller.resetNotifications = function () {
        controller.errorMessage = null;
        controller.hasError = false;
        controller.confirmationMessage = null;
        controller.hasConfirmation = false;
    };

    controller.init();
}])
.controller("onboardingStepThreeController", ["$scope", "Azureservice", "localStorageService", "$rootScope", "ProductFileCategories", "$state", "$stateParams", "$http", "Countries","$timeout", function ($scope, Azureservice, localStorageService, $rootScope, ProductFileCategories, $state, $stateParams, $http,Countries,$timeout) {
    var controller = this;
    controller.statusDto = {};
    $scope.user = {};
    controller.productId = $stateParams.productId;
    controller.flowOptions = {
        target: "/api/ProductFileUpload",
        permanentErrors: [500, 501],
        maxChunkRetries: 1,
        chunkRetryInterval: 2000,
        simultaneousUploads: 4,
        progressCallbacksInterval: 500,
        headers: function () {
            var authData = localStorageService.get("authorizationData");

            return { "Authorization": "Bearer " + authData.token };
        },
        generateUniqueIdentifier: function () {
            return (new Date()).getTime();
        }
    };
    controller.validationErrors = [];
    controller.busy = false;
    controller.busyPrev = false;
    controller.products = [];
    controller.productFileCategories = ProductFileCategories;
    controller.productFiles = [];
    controller.productFileIndex = 0;
    controller.showUrlUploadPopover = false;
    controller.uploadUrl = null;
    controller.countries = Countries;
    //$scope.country.name = "Afghanistan";
    controller.init = function () {
        var authData = localStorageService.get("authorizationData");
        controller.busy = false;
        Azureservice.invokeApi("GetMyOnboardingStatus", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (onboardingStatus) {
              if (onboardingStatus.OnboardingSkipped) {
                  $state.go("dashboard");

                  return;
              }

              if (!onboardingStatus.OnboardingStep) {
                  return;
              }

              if (onboardingStatus.OnboardingStep === 1) {
                  $state.go("onboardingStepOne");

                  return;
              }

              if (onboardingStatus.OnboardingStep === 2) {
                  $state.go("onboardingStepTwo");

                  return;
              }

              if (onboardingStatus.OnboardingStep === 4) {
                  $state.go("onboardingStepFour");

                  return;
              }

              Azureservice.invokeApi("GetMyCompanyProducts", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
                .then(function (products) {
                    controller.products = products;
                    $scope.$broadcast("dataready", null);
                });

          });
            Azureservice.invokeApi("GetMyPersonalProfile", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
                  .then(function (user) {
                      
                      $scope.user = user;
                      $scope.Individual = user.IsIndividual;
                      $scope.$broadcast("dataready", null);
                  });

        //$('[data-toggle="popover"]').popover()
    };

    $scope.filterValue = function ($event) {
        if (isNaN(String.fromCharCode($event.keyCode))) {
            $event.preventDefault();
        }
    };

    $scope.setUserCountry = function (country) {
        $scope.user.Country = country;
    }

    controller.getUserProfileImage = function () {
        
        if (!$scope.user.Image) {
            return "/img/user_image.svg";
        }

        return "data:image/JPEG;base64," + $scope.user.Image;
    };
    controller.flowFilesSubmitted = function ($files, $event, $flow) {
        if (!$event) {
            return;
        }

        var i;
        var reader = new FileReader();
        var base64DataBeginMarker = ";base64,";
        var base64DataBeginIndex;

        reader.addEventListener("load", function () {
            base64DataBeginIndex = reader.result.indexOf(base64DataBeginMarker);
            $scope.user.Image = reader.result.substring(base64DataBeginIndex + base64DataBeginMarker.length);
            controller.ImageType = $files[0].getType();
            $flow.cancel();
            controller.hasImagesToUpload = true;
            $scope.$apply();
        }, false);
        reader.readAsDataURL($files[0].file);
    };

    controller.toggleUrlUploadPopover = function () {
        controller.showUrlUploadPopover = !controller.showUrlUploadPopover;
    };

    controller.arrayBufferToBase64String = function (arrayBuffer) {
        var binary = "";
        var bytes = new Uint8Array(arrayBuffer);
        var len = bytes.byteLength;

        for (var i = 0; i < len; i++) {
            binary += String.fromCharCode(bytes[i]);
        }

        return window.btoa(binary);
    }

    controller.getFileFromUrl = function () {
        if (!controller.uploadUrl || controller.uploadUrl.length === 0) {
            return;
        }

        var authData = localStorageService.get("authorizationData");

        controller.busy = true;
        $http.get("/api/GetFileFromUrl?url=" + encodeURIComponent(controller.uploadUrl), {
            responseType: "arraybuffer"
        }).success(function (data, status, headers) {
            var base64 = controller.arrayBufferToBase64String(data);
            var contentType = headers["content-type"] || "application/octet-stream";
            var productFile = {
                Index: controller.productFileIndex++,
                Name: controller.uploadUrl,
                //Type: file.type,
                Size: base64.length,
                Image: null,
                ImageType: null,
                Editing: false,
                Bytes: base64
            };

            controller.productFiles.push(productFile);
            controller.showUrlUploadPopover = false;
            controller.uploadUrl = null;
            controller.busy = false;
        });
    };

    controller.skipOnboarding = function () {
        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("SkipOnboarding", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (onboardingStatus) {
              $state.go("dashboard");
          });
    };

    controller.nextStep = function () {
        var authData = localStorageService.get("authorizationData");

        controller.busy = true;
        //Azureservice.invokeApi("UploadProductFiles", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: controller.productFiles })
        //    .then(function (productFiles) {
        //        controller.productFiles = productFiles;
        //        controller.statusDto.OnboardingStep = 4;
        //        Azureservice.invokeApi("SetMyCurrentOnboardingStep", { method: "POST", body: controller.statusDto, headers: { "Authorization": "Bearer " + authData.token } })
        //          .then(function (onboardingStatus) {
        //              $state.go("onboardingStepFour");
        //          });
        //    })
        //    .finally(function () {
        //        controller.busy = false;
        //    });

        Azureservice.invokeApi("UpdateMyPersonalProfile", { method: "POST", headers: { "Authorization": "Bearer " + authData.token }, body: $scope.user })
            .then(function (user) {
                controller.user = user;
                controller.statusDto.OnboardingStep = 4;
                Azureservice.invokeApi("SetMyCurrentOnboardingStep", { method: "POST", body: controller.statusDto, headers: { "Authorization": "Bearer " + authData.token } })
                          .then(function (onboardingStatus) {
                              $state.go("onboardingStepFour");
                          });
                
                //if (controller.hasImagesToUpload !== true) {
                //    $timeout(function () {
                //        $rootScope.$emit("UserProfileImageChanged");
                //    }, 1000);
                //}

                controller.hasImagesToUpload = false;
            })
        .finally(function () {
            controller.busy = false;
        });
    };

    controller.backStep = function () {
        controller.statusDto.OnboardingStep = 2;
        controller.busy = false;
        controller.busyPrev = true;
        var authData = localStorageService.get("authorizationData");
        Azureservice.invokeApi("SetMyCurrentOnboardingStep", { method: "POST", body: controller.statusDto, headers: { "Authorization": "Bearer " + authData.token } })
            .then(function (onboardingStatus) {
                $state.go("onboardingStepTwo");
            });
    };

    controller.getProductFileImage = function (productFile) {

        if (!productFile.Image && !productFile.Bytes) {
            return "http://placehold.it/212x212";
        }

        var isPdf = false;
        var isExcel = false;
        var isDoc = false;
        var isPPT = false;

        if (productFile.Name.indexOf(".pdf") != -1) {
            isPdf = true;
        }

        if (productFile.Name.indexOf(".ppt") != -1) {
            isPPT = true;
        }

        if (productFile.Name.indexOf(".pptx") != -1) {
            isPPT = true;
        }

        if (productFile.Name.indexOf(".doc") != -1) {
            isDoc = true;
        }

        if (productFile.Name.indexOf(".docx") != -1) {
            isDoc = true;
        }
        if (productFile.Name.indexOf(".xls") != -1) {
            isExcel = true;
        }
        if (productFile.Name.indexOf(".xlsx") != -1) {
            isExcel = true;
        }

        if (isPdf) {
            return "/img/pdf.jpg";
        }
        else if (isExcel) {
            return "/img/excel.png";
        }
        else if (isDoc) {
            return "/img/docx.png";
        }
        else if (isPPT) {
            return "/img/ppt.png";
        }
        else {
            if (productFile.Bytes) {
                return "data:image/JPEG;base64," + productFile.Bytes;
            }
            else {
                return "data:image/JPEG;base64," + productFile.Image;
            }
        }
    };


    controller.flowProductFilesSubmitted = function ($files, $event, $flow) {
        if (!$event || !$files) {
            return;
        }

        var len = $files.length;

        if (!len) {
            return;
        }

        for (var i = 0; i < len; i++) {
            var reader = new FileReader();

            reader.addEventListener("load", function (file) {
                var base64DataBeginMarker = ";base64,";
                var base64DataBeginIndex = reader.result.indexOf(base64DataBeginMarker);
                var productFile = {
                    Index: controller.productFileIndex++,
                    Name: file.name,
                    Type: file.type,
                    Size: file.size,
                    Image: null,
                    ImageType: null,
                    Editing: false,
                    Bytes: reader.result.substring(base64DataBeginIndex + base64DataBeginMarker.length)
                };

                controller.productFiles.push(productFile);
                $flow.cancel();
                $scope.$apply();
            }.bind(null, $files[i].file), false);
            reader.readAsDataURL($files[i].file);
        }
    };

    controller.flowProductFileImageSubmitted = function ($files, $event, $flow, productFile) {
        if (!$event || !$files) {
            return;
        }

        var len = $files.length;

        if (!len) {
            return;
        }

        var reader = new FileReader();
        var base64DataBeginMarker = ";base64,";
        var base64DataBeginIndex;

        reader.addEventListener("load", function () {
            base64DataBeginIndex = reader.result.indexOf(base64DataBeginMarker);
            productFile.Image = reader.result.substring(base64DataBeginIndex + base64DataBeginMarker.length);
            productFile.ImageType = $files[0].getType();
            $flow.cancel();
            $scope.$apply();
        }, false);
        reader.readAsDataURL($files[0].file);
    };

    controller.base64StringToBlob = function (base64String, contentType, sliceSize) {
        contentType = contentType || "";
        sliceSize = sliceSize || 512;

        var byteCharacters = atob(base64String);
        var byteArrays = [];
        var slice;
        var byteNumbers;
        var byteArray;
        var blob;

        for (var offset = 0; offset < byteCharacters.length; offset += sliceSize) {
            slice = byteCharacters.slice(offset, offset + sliceSize);
            byteNumbers = new Array(slice.length);

            for (var i = 0; i < slice.length; i++) {
                byteNumbers[i] = slice.charCodeAt(i);
            }

            byteArray = new Uint8Array(byteNumbers);
            byteArrays.push(byteArray);
        }

        blob = new Blob(byteArrays, { type: contentType });

        return blob;
    };

    controller.removeProductFile = function (productFile) {
        for (var i = 0, len = controller.productFiles.length; i < len; i++) {
            if (controller.productFiles[i].Index === productFile.Index) {
                controller.productFiles.splice(i, 1);

                return;
            }
        }
    };

    controller.toggleEditProductFile = function (productFile) {
        productFile.Editing = !productFile.Editing;
    };

    controller.setProductFileCategory = function (productFileCategory, productFile) {
        productFile.Category = productFileCategory.code;
    };

    controller.getProductFileCategoryName = function (productFile, valueIfNull) {
        for (var i = 0, len = controller.productFileCategories.length; i < len; i++) {
            if (controller.productFileCategories[i].code === productFile.Category) {
                return controller.productFileCategories[i].name;
            }
        }

        return valueIfNull;
    };

    controller.setProductFileProduct = function (product, productFile) {
        productFile.ProductID = product.ID;
    };

    controller.getProductFileProductName = function (productFile, valueIfNull) {
        if (!productFile.ProductID) {
            return valueIfNull;
        }

        for (var i = 0, len = controller.products.length; i < len; i++) {
            if (controller.products[i].ID === productFile.ProductID) {
                return controller.products[i].Name;
            }
        }

        return valueIfNull;
    };

    controller.init();

    
}])
.controller("onboardingStepFourController", ["$scope", "$state", "Azureservice", "localStorageService", "InviteExistingUser", function ($scope, $state, Azureservice, localStorageService,InviteExistingUser) {
    var controller = this;
    controller.statusDto = {};
    controller.init = function () {
        var authData = localStorageService.get("authorizationData");
        
        Azureservice.invokeApi("GetAllUsers", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
            .then(function (UserList) {
                
                $scope.Users = UserList;
 });

        Azureservice.invokeApi("GetMyOnboardingStatus", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (onboardingStatus) {
              if (onboardingStatus.OnboardingSkipped) {
                  $state.go("dashboard");

                  return;
              }

              if (!onboardingStatus.OnboardingStep) {
                  return;
              }

              if (onboardingStatus.OnboardingStep === 1) {
                  $state.go("onboardingStepOne");

                  return;
              }

              if (onboardingStatus.OnboardingStep === 2) {
                  $state.go("onboardingStepTwo");
                  return;
              }

              if (onboardingStatus.OnboardingStep === 3) {
                  $state.go("onboardingStepThree");
                  return;
              }
              if (onboardingStatus.OnboardingStep === 4) {
                  $state.go("onboardingStepFour");
                  return;
              }

              Azureservice.invokeApi("CompleteOnboarding", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
                .then(function () {
                    return;
                });
          });
    };

    controller.skipOnboarding = function () {
        var authData = localStorageService.get("authorizationData");

        Azureservice.invokeApi("SkipOnboarding", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } })
          .then(function (onboardingStatus) {
              $state.go("dashboard");
          });
    };

    controller.search = function (searchQuery) {
        var authData = localStorageService.get("authorizationData");

        return Azureservice.invokeApi("SearchCompaniesAndProducts?query=" + encodeURIComponent(searchQuery) + "&maxRecords=10", { method: "GET", headers: { "Authorization": "Bearer " + authData.token } });
    };

    controller.showProducts = function () {
        controller.companiesVisible = false;
    };

    $scope.InviteUser = function (Id)
    {
        debugger
        var Result = InviteExistingUser.InviteUser(Id);

        Result.then(function (Data) {
            debugger
        });
    }

    controller.showCompanies = function () {
        controller.companiesVisible = true;
    };



    controller.backStep = function () {
        controller.statusDto.OnboardingStep = 3;
        var authData = localStorageService.get("authorizationData");
        Azureservice.invokeApi("SetMyCurrentOnboardingStep", { method: "POST", body: controller.statusDto, headers: { "Authorization": "Bearer " + authData.token } })
            .then(function (onboardingStatus) {
                $state.go("onboardingStepThree");
            });
    };

    controller.getUserProfileImage = function () {
        
        if (!$scope.Users.Image) {
            return "/img/user_image.svg";
        }

        return "data:image/JPEG;base64," + controller.user.Image;
    };

    controller.init();
}])
.run(["$rootScope", "$state", "localStorageService", "$timeout", function ($rootScope, $state, localStorageService, $timeout) {
    var authData = localStorageService.get("authorizationData");

    if ((authData) && (authData.userName)) { //TODO: Implement OAuth refresh tokens.
        $timeout(function () {
            $rootScope.$emit("LoginEvent", authData.userName);
        }, 1000);
    }

    $rootScope.$on("$stateChangeError", function (event) {
        $state.go("404");
    });

    $rootScope.$on("ShouldLoginEvent", function (event) {
        var authData = localStorageService.get("authorizationData");

        if ((authData) && (authData.userName)) {
            return;
        }

        event.preventDefault();
        LoginModal()
            .then(function () {
                $state.transitionTo(toState.name, null, { notify: false });

                return $state.go(toState.name, toParams);
            })
            .catch(function () {
                return $state.go("home");
            });
    });

    $rootScope.$on("$stateChangeStart", function (event, toState, toParams, fromState) {
        var requireLogin = toState.data.requireLogin;

        if (requireLogin !== true) {
            return;
        }

        var authData = localStorageService.get("authorizationData");

        if ((authData) && (authData.userName) && (authData.tokenExpirationDate)) {
            var tokenExpirationDate = moment(authData.tokenExpirationDate);

            if (tokenExpirationDate && tokenExpirationDate.isAfter()) {
                return;
            }
        }

        event.preventDefault();
        $rootScope.toState = toState.name;
        $rootScope.toParams = toParams;
        $state.go("login");
        /*
        LoginModal()
            .then(function () {
                $state.transitionTo(toState.name, null, { notify: false });

                return $state.go(toState.name, toParams);
            })
            .catch(function () {
                return $state.go("home");
            });
        */
    });

    $rootScope.$on("$stateChangeSuccess", function (event, toState, toStateParams, fromState, formStateParams) {
        document.body.scrollTop = document.documentElement.scrollTop = 0;
        $rootScope.previousState = fromState;
    });
}]);