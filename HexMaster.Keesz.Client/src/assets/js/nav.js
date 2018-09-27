!(function(e) {
  'use strict';

  e(document).on('click', '#sidenavToggler', function(o) {
    o.preventDefault(),
      e('#mainNav').toggleClass('collapsed'),
      e('.navbar-sidenav .sidenav-second-level, .navbar-sidenav .sidenav-third-level').removeClass('show');
  }),
    e('.navbar-sidenav .nav-link-collapse').click(function(o) {
      o.preventDefault(), e('#mainNav').removeClass('collapsed');
    });
})(jQuery);
