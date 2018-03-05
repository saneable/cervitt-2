angular.module("karibouAlphaApp")
    .config(["$stateProvider", "$urlRouterProvider", function ($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise("/");

        $stateProvider
            .state("home", {
                url: "/",
                templateUrl: "home_view.html",
                data: {
                    requireLogin: false
                },                		
                views: {		
                    "main": {		
                        templateUrl: "home_view.html",
                          controller: "loginController",
                          controllerAs: "ctrl"
                    }
                }
            })
            .state("404", {
                url: "/404",
                templateUrl: "404_view.html",
                data: {
                    requireLogin: false
                }
            })
            .state("login", {
                url: "/login",
                data: {
                    requireLogin: false
                },
                views: {		
                    "main": {		
                        templateUrl: "home_view.html",
                        controller: "loginController",
                        controllerAs: "ctrl"
                    }
                }
            })
            .state("logout", {
                url: "/logout",
                data: {
                    requireLogin: false
                },
                views: {
                    "main": {
                        
                        controller: "LogouController",
                        controllerAs: "ctrl"
                    }
                }
            })
             .state("confirmemail", {
                 url: "/confirmemail",
                 data: {
                     requireLogin: false
                 },
                 views: {
                     "main": {
                                templateUrl: "ConfirmEmail.html",
                             }		                    
                        }		                
				
             })
            .state("emailalreadyconfirmed", {
                url: "/emailalreadyconfirmed",
                data: {
                    requireLogin: false
                },
                views: {
                    "main": {
                        templateUrl: "ConfirmEmailAlready.html",
                    }
                }

            })
             .state("ForgotPassword", {
                 url: "/ForgotPassword",
                 data: {
                     requireLogin: false
                 },
                 views: {
                     "main": {
                                templateUrl: "Forgot_Password.html",
                                controller: "ForgotPasswordController",
                                controllerAs: "ctrl"	

                             }		                    
                        }		                
				
             })
            .state("VarificationCode", {
                url: "/VarificationCode",
                data: {
                    requireLogin: false
                },
                views: {
                    "main": {
                        templateUrl: "AddCode.html",
                        controller: "ForgotPasswordController",
                        controllerAs: "ctrl"

                    }
                }

            })
            .state("registration", {
                url: "/registration/:Code",
                data: {
                    requireLogin: false
                },
                views: {		
                    "main": {		
                        templateUrl: "registration_view.html",
                        controller: "signupController",
                        controllerAs: "ctrl"
                    }
                }
            })
            .state("linkedin", {
                url: "/linkedin",
                data: {
                    requireLogin: false
                },
                views: {
                    "main": {
                        templateUrl: "linkedin.html",
                        controller: "linkedInController",
                        controllerAs: "ctrl"
                    }
                }
            })
            .state("associate", {
                url: "/associate",
                data: {
                    requireLogin: false
                },
                views: {
                    "main": {
                        templateUrl: "associate.html",
                        controller: "associateController",
                        controllerAs: "ctrl"
                    }
                }
            })
            .state("search", {
                url: "/search/:query/:show",
                data: {
                    requireLogin: true
                },
                views: {
                    "main": {
                        templateUrl: "search_view.html",
                        controller: "searchController",
                        controllerAs: "ctrl"
                    },
                    "header": {
                        templateUrl: "loggedin_header_view.html",
                        controller: "loggedInHeaderController",
                        controllerAs: "ctrl"
                    }
                }
            })
            .state("personalProfileDetails", {
                url: "/personal_profile_details",
                data: {
                    requireLogin: true
                },
                views: {
                    "main": {		
                        templateUrl: "personal_profile_details_view.html",
                        controller: "personalProfileDetailsController",
                        controllerAs: "ctrl"
                    },
                    "header": {
                        templateUrl: "loggedin_header_view.html",
                        controller: "loggedInHeaderController",
                        controllerAs: "ctrl"
                    }
                }
            })
            .state("personalProfileNotifications", {
                url: "/personal_profile_notifications",
                data: {
                    requireLogin: true
                },
                views: {		
                    "main": {		
                        templateUrl: "personal_profile_notifications_view.html",
                        controller: "personalProfileNotificationsController",
                        controllerAs: "ctrl"
                    },
                    "header": {
                        templateUrl: "loggedin_header_view.html",
                        controller: "loggedInHeaderController",
                        controllerAs: "ctrl"
                    }
                }
            })
            .state("personalProfileSocialMedia", {
                url: "/personal_profile_social_media",
                data: {
                    requireLogin: true
                },
                views: {		
                    "main": {		
                        templateUrl: "personal_profile_social_media_view.html",
                        controller: "personalProfileSocialMediaController",
                        controllerAs: "ctrl"
                    },
                    "header": {
                        templateUrl: "loggedin_header_view.html",
                        controller: "loggedInHeaderController",
                        controllerAs: "ctrl"
                    }
                }
            })
            .state("companyProfileDetails", {
                url: "/company_profile_details",
                data: {
                    requireLogin: true
                },
                views: {		
                    "main": {		
                        templateUrl: "company_profile_details_view.html",
                        controller: "companyProfileDetailsController",
                        controllerAs: "ctrl"
                    },
                    "header": {
                        templateUrl: "loggedin_header_view.html",
                        controller: "loggedInHeaderController",
                        controllerAs: "ctrl"
                    }
                }
            })
            .state("companyProfileNews", {
                url: "/company_profile_news",
                data: {
                    requireLogin: true
                },
                views: {
                    "main": {
                        templateUrl: "company_profile_details_news.html",
                        controller: "dashboardController",
                        controllerAs: "ctrl"
                    },
                    "header": {
                        templateUrl: "loggedin_header_view.html",
                        controller: "loggedInHeaderController",
                        controllerAs: "ctrl"
                    },
                    
                }
            })
            .state("companyProfileTeam", {
                url: "/company_profile_team",
                data: {
                    requireLogin: true
                },
                views: {		
                    "main": {		
                        templateUrl: "company_profile_team_view.html",
                        controller: "companyProfileTeamController",
                        controllerAs: "ctrl"
                    },
                    "header": {
                        templateUrl: "loggedin_header_view.html",
                        controller: "loggedInHeaderController",
                        controllerAs: "ctrl"
                    }
                }
            })
            .state("companyProfileFollowerGroups", {
                url: "/company_profile_follower_groups",
                data: {
                    requireLogin: true
                },
                views: {
                    "main": {
                        templateUrl: "company_profile_follower_groups_view.html",
                        controller: "companyProfileFollowerGroupsController",
                        controllerAs: "ctrl"
                    },
                    "header": {
                        templateUrl: "loggedin_header_view.html",
                        controller: "loggedInHeaderController",
                        controllerAs: "ctrl"
                    }
                }
            })
            .state("companyConnections", {
                url: "/company_connections",
                data: {
                    requireLogin: true
                },
                views: {		
                    "main": {		
                        templateUrl: "company_connections_view.html",
                        controller: "companyConnectionsController",
                        controllerAs: "ctrl"
                    },
                    "header": {
                        templateUrl: "loggedin_header_view.html",
                        controller: "loggedInHeaderController",
                        controllerAs: "ctrl"
                    }
                }
            })
            .state("personalConnections", {
                url: "/personal_connections",
                data: {
                    requireLogin: true
                },
                views: {		
                    "main": {		
                        templateUrl: "personal_connections_view.html",
                        controller: "personalConnectionsController",
                        controllerAs: "ctrl"
                    },
                    "header": {
                        templateUrl: "loggedin_header_view.html",
                        controller: "loggedInHeaderController",
                        controllerAs: "ctrl"
                    }
                }
            })
            .state("companyProfileNotifications", {
                url: "/company_profile_notifications",
                data: {
                    requireLogin: true
                },
                views: {		
                    "main": {		
                        templateUrl: "company_profile_notifications_view.html",
                        controller: "companyProfileNotificationsController",
                        controllerAs: "ctrl"
                    },
                    "header": {
                        templateUrl: "loggedin_header_view.html",
                        controller: "loggedInHeaderController",
                        controllerAs: "ctrl"
                    }
                }
            })
            .state("companyProfileSocialMedia", {
                url: "/company_profile_social_media",
                data: {
                    requireLogin: true
                },
                views: {		
                    "main": {		
                        templateUrl: "company_profile_social_media_view.html",
                        controller: "companyProfileSocialMediaController",
                        controllerAs: "ctrl"
                    },
                    "header": {
                        templateUrl: "loggedin_header_view.html",
                        controller: "loggedInHeaderController",
                        controllerAs: "ctrl"
                    }
                }
            })
            .state("productDetails", {
                url: "/product_details/:productId",
                data: {
                    requireLogin: true
                },
                views: {		
                    "main": {		
                        templateUrl: "product_details_view.html",
                        controller: "productDetailsController",
                        controllerAs: "ctrl"
                    },
                    "header": {
                        templateUrl: "loggedin_header_view.html",
                        controller: "loggedInHeaderController",
                        controllerAs: "ctrl"
                    }
                }
            })
            .state("productTeam", {
                url: "/product_team/:productId",
                data: {
                    requireLogin: true
                },
                views: {		
                    "main": {		
                        templateUrl: "product_team_view.html",
                        controller: "productTeamController",
                        controllerAs: "ctrl"
                    },
                    "header": {
                        templateUrl: "loggedin_header_view.html",
                        controller: "loggedInHeaderController",
                        controllerAs: "ctrl"
                    }
                }
            })
            .state("productFiles", {
                url: "/product_files/:productId",
                data: {
                    requireLogin: true
                },
                views: {		
                    "main": {		
                        templateUrl: "product_files_view.html",
                        controller: "productFilesController",
                        controllerAs: "ctrl"
                    },
                    "header": {
                        templateUrl: "loggedin_header_view.html",
                        controller: "loggedInHeaderController",
                        controllerAs: "ctrl"
                    }
                }
            })
            .state("productFileUpload", {
                url: "/product_file_upload/:productId",
                data: {
                    requireLogin: true
                },
                views: {
                    "main": {
                        templateUrl: "product_file_upload_view.html",
                        controller: "productFileUploadController",
                        controllerAs: "ctrl"
                    },
                    "header": {
                        templateUrl: "loggedin_header_view.html",
                        controller: "loggedInHeaderController",
                        controllerAs: "ctrl"
                    }
                }
            })
            .state("dashboard", {
                url: "/dashboard?showArticleEditor",
                data: {
                    requireLogin: true
                },
                views: {
                    "main": {
                        templateUrl: "dashboard_view.html",
                        controller: "dashboardController",
                        controllerAs: "ctrl"
                    },
                    "header": {
                        templateUrl: "loggedin_header_view.html",
                        controller: "loggedInHeaderController",
                        controllerAs: "ctrl"
                    }
                }
            })
            .state("dashboardFeedback", {
                url: "/dashboard_feedback",
                data: {
                    requireLogin: true
                },
                views: {
                    "main": {
                        templateUrl: "dashboard_feedback_view.html",
                        controller: "dashboardFeedbackController",
                        controllerAs: "ctrl"
                    },
                    "header": {
                        templateUrl: "loggedin_header_view.html",
                        controller: "loggedInHeaderController",
                        controllerAs: "ctrl"
                    }
                }
            })
            .state("dashboardProductUpdates", {
                url: "/dashboard_product_updates",
                data: {
                    requireLogin: true
                },
                views: {
                    "main": {
                        templateUrl: "dashboard_product_updates_view.html",
                        controller: "dashboardProductUpdatesController",
                        controllerAs: "ctrl"
                    },
                    "header": {
                        templateUrl: "loggedin_header_view.html",
                        controller: "loggedInHeaderController",
                        controllerAs: "ctrl"
                    }
                }
            })
            .state("myCompany", {
                url: "/my_company",
                data: {
                    requireLogin: true
                },
                views: {
                    "main": {
                        templateUrl: "my_company_view.html",
                        controller: "myCompanyController",
                        controllerAs: "ctrl"
                    },
                    "header": {
                        templateUrl: "loggedin_header_view.html",
                        controller: "loggedInHeaderController",
                        controllerAs: "ctrl"
                    }
                }
            })
            .state("company", {
                url: "/company/:companyId",
                data: {
                    requireLogin: true
                },
                views: {
                    "main": {
                        templateUrl: "company_view.html",
                        controller: "companyController",
                        controllerAs: "ctrl"
                    },
                    "header": {
                        templateUrl: "loggedin_header_view.html",
                        controller: "loggedInHeaderController",
                        controllerAs: "ctrl"
                    }

                    //"header": {
                    //    templateUrl: "loggedin_company_and_product_header_view.html",
                    //    controller: "loggedInCompanyAndProductHeaderController",
                    //    controllerAs: "ctrl"
                    //}
                }
            })
            .state("companyByUri", {
                url: "/c/:companyUri",
                data: {
                    requireLogin: true
                },
                views: {
                    "main": {
                        templateUrl: "company_view.html",
                        controller: "companyByUriController",
                        controllerAs: "ctrl"
                    },
                    "header": {
                        templateUrl: "loggedin_company_and_product_header_view.html",
                        controller: "loggedInCompanyAndProductHeaderController",
                        controllerAs: "ctrl"
                    }
                }
            })
            .state("myProduct", {
                url: "/my_product/:productId",
                data: {
                    requireLogin: true
                },
                views: {
                    "main": {
                        templateUrl: "my_product_view.html",
                        controller: "myProductController",
                        controllerAs: "ctrl"
                    },
                    "header": {
                        templateUrl: "loggedin_header_view.html",
                        controller: "loggedInHeaderController",
                        controllerAs: "ctrl"
                    }
                }
            })
            .state("product", {
                url: "/product/:productId/:companyId",
                data: {
                    requireLogin: true
                },
                views: {
                    "main": {
                        templateUrl: "product_view.html",
                        controller: "productController",
                        controllerAs: "ctrl"
                    },
                    "header": {
                        templateUrl: "loggedin_company_and_product_header_view.html",
                        controller: "loggedInCompanyAndProductHeaderController",
                        controllerAs: "ctrl"
                    }
                }
            })
            .state("productByUri", {
                url: "/p/:companyUri/:productUri",
                data: {
                    requireLogin: true
                },
                views: {
                    "main": {
                        templateUrl: "product_view.html",
                        controller: "productByUriController",
                        controllerAs: "ctrl"
                    },
                    "header": {
                        templateUrl: "loggedin_company_and_product_header_view.html",
                        controller: "loggedInCompanyAndProductHeaderController",
                        controllerAs: "ctrl"
                    }
                }
            })
            .state("onboarding", {
                url: "/onboarding_main",
                data: {
                    requireLogin: true
                },
                views: {
                    "main": {
                        templateUrl: "onboarding_main_view.html",
                        controller: "onboardingController",
                        controllerAs: "ctrl"
                    },
                    "header": {
                        templateUrl: "loggedin_header_view.html",
                        controller: "loggedInHeaderController",
                        controllerAs: "ctrl"
                    }
                }
            })
            .state("Tags", {
                url: "/Tags",
                data: {
                    requireLogin: false
                },
                views: {
                    "main": {
                        templateUrl: "Tags.html",
                        controller: "onboardingStepOneController",
                        controllerAs: "ctrl"
                    }
                }
            })
            .state("onboardingStepOne", {
                url: "/onboarding_step_one",
                data: {
                    requireLogin: true
                },
                views: {
                    "main": {
                        templateUrl: "onboarding_step_one_view.html",
                        controller: "onboardingStepOneController",
                        controllerAs: "ctrl"
                    },
                    "header": {
                        templateUrl: "loggedin_header_view.html",
                        controller: "loggedInHeaderController",
                        controllerAs: "ctrl"
                    }
                }
            })
            .state("onboardingStepTwo", {
                url: "/onboarding_step_two",
                data: {
                    requireLogin: true
                },
                views: {
                    "main": {
                        templateUrl: "onboarding_step_two_view.html",
                        controller: "onboardingStepTwoController",
                        controllerAs: "ctrl"
                    },
                    "header": {
                        templateUrl: "loggedin_header_view.html",
                        controller: "loggedInHeaderController",
                        controllerAs: "ctrl"
                    }
                }
            })
            .state("onboardingStepThree", {
                url: "/onboarding_step_three",
                data: {
                    requireLogin: true
                },
                views: {
                    "main": {
                        templateUrl: "onboarding_step_three_view.html",
                        controller: "onboardingStepThreeController",
                        controllerAs: "ctrl"
                    },
                    "header": {
                        templateUrl: "loggedin_header_view.html",
                        controller: "loggedInHeaderController",
                        controllerAs: "ctrl"
                    }
                }
            })
            .state("onboardingStepFour", {
                url: "/onboarding_step_four",
                data: {
                    requireLogin: true
                },
                views: {
                    "main": {
                        templateUrl: "onboarding_step_four_view.html",
                        controller: "onboardingStepFourController",
                        controllerAs: "ctrl"
                    },
                    "header": {
                        templateUrl: "loggedin_header_view.html",
                        controller: "loggedInHeaderController",
                        controllerAs: "ctrl"
                    }
                }
            })
            .state("onboardingCompleted", {
                url: "/onboarding_completed",
                data: {
                    requireLogin: true
                },
                views: {
                    "main": {
                        templateUrl: "onboarding_completed.html",
                        controller: "onboardingCompletedController",
                        controllerAs: "ctrl"
                    },
                    "header": {
                        templateUrl: "loggedin_header_view.html",
                        controller: "loggedInHeaderController",
                        controllerAs: "ctrl"
                    }
                }
            })
            //.state("resetPassword", {
            //    url: "/reset_password/:userId/:passwordResetToken",
            //    templateUrl: "reset_password_view.html",
            //    controller: "ForgotPasswordController",
            //    controllerAs: "ctrl",
            //    data: {
            //        requireLogin: false
            //    }
            //})

               
               
//:id/:token
            .state("reset_password", {
                 url: "/reset_password",
                data: {
                    requireLogin: false
                },
                views: {
                    "main": {
                        templateUrl: "ResetPassword.html",
                        controller: "ForgotPasswordController",
                        controllerAs: "ctrl",
                    }
                }
            })

        .state("demo", {
            url: "/demo",
            data: {
                requireLogin: false
            },
            views: {
                "main": {
                    templateUrl: "demo.html",

                }
            }
        })
            .state("security", {
                url: "/security",
                data: {
                    requireLogin: false
                },
                views: {
                    "main": {
                        templateUrl: "security.html",

                    }
                }
            })
            .state("terms", {
                url: "/terms",
                data: {
                    requireLogin: false
                },
                views: {
                    "main": {
                        templateUrl: "terms.html",

                    }
                }
            })
         .state("policy", {
             url: "/policy",
             data: {
                 requireLogin: false
             },
             views: {
                 "main": {
                     templateUrl: "policy.html",

                 }
             }
         })
         .state("pricing", {
             url: "/pricing",
             data: {
                 requireLogin: false
             },
             views: {
                 "main": {
                     templateUrl: "pricing.html",

                 }
             }
         })
         .state("privacy", {
             url: "/privacy",
             data: {
                 requireLogin: false
             },
             views: {
                 "main": {
                     templateUrl: "privacy.html",

                 }
             }
         })
         //.state("security", {
         //    url: "/security",
         //    data: {
         //        requireLogin: false
         //    },
         //    views: {
         //        "main": {
         //            templateUrl: "security.html",

         //        }
         //    }
         //})
        //.state("terms", {
        //    url: "/terms",
        //    data: {
        //        requireLogin: false
        //    },
        //    views: {
        //        "main": {
        //            templateUrl: "terms.html",

        //        }

        //    }
        //})

            .state("CodeSentMessage", {
                url: "/CodeSentMessage",
                data: {
                    requireLogin: false
                },
                views: {
                    "main": {
                        templateUrl: "CodeSentMessage.html",
                        //controller: "onboardingStepOneController"
                    }

                }
            })
            .state("PasswordResetSuccess", {
                url: "/PasswordResetSuccess",
                data: {
                    requireLogin: false
                },
                views: {
                    "main": {
                        templateUrl: "PasswordResetSuccess.html",
                        //controller: "onboardingStepOneController"
                    }

                }
            })
        .state("test", {
            url: "/test",
            data: {
                requireLogin: false
            },
            views: {
                "main": {
                    templateUrl: "AddSector.html",
                    controller: "onboardingStepOneController"
                }

            }
        })
       

    }]);