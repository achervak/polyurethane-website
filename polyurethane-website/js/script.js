$(document).ready(function(){
	//-- Click on detail
	$("ul.menu-items > li").on("click",function(){
		$("ul.menu-items > li").removeClass("active");
		$(this).addClass("active");
	})

	$(".attr,.attr2").on("click",function(){
		var clase = $(this).attr("class");

		$("." + clase).removeClass("active");
		$(this).addClass("active");
	})

	//-- Click on QUANTITY
	$(".btn-minus").on("click",function(){
		var now = $(".section > div > input").val();
		if ($.isNumeric(now)){
			if (parseInt(now) -1 > 0){ now--;}
			$(".section > div > input").val(now);
		}else{
			$(".section > div > input").val("1");
		}
	})            
	$(".btn-plus").on("click",function(){
		var now = $(".section > div > input").val();
		if ($.isNumeric(now)){
			$(".section > div > input").val(parseInt(now)+1);
		}else{
			$(".section > div > input").val("1");
		}
	})                        
}) 







/**

*	The important attributes are 'data-action="filter"' and 'data-filters="#table-selector"'
*/
(function(){
	'use strict';
	var $ = jQuery;
	$.fn.extend({
		filterTable: function(){
			return this.each(function(){
				$(this).on('keyup', function(e){
					$('.filterTable_no_results').remove();
					var $this = $(this), 
						search = $this.val().toLowerCase(), 
						target = $this.attr('data-filters'), 
						$target = $(target), 
						$rows = $target.find('tbody tr');

					if(search == '') {
						$rows.show(); 
					} else {
						$rows.each(function(){
							var $this = $(this);
							$this.text().toLowerCase().indexOf(search) === -1 ? $this.hide() : $this.show();
						})
						if($target.find('tbody tr:visible').size() === 0) {
							var col_count = $target.find('tr').first().find('td').size();
							var no_results = $('<tr class="filterTable_no_results"><td colspan="'+col_count+'">No results found</td></tr>')
							$target.find('tbody').append(no_results);
						}
					}
				});
			});
		}
	});
	$('[data-action="filter"]').filterTable();
})(jQuery);

$(function(){
	// attach table filter plugin to inputs
	$('[data-action="filter"]').filterTable();

	$('.container').on('click', '.panel-heading span.filter', function(e){
		var $this = $(this), 
			$panel = $this.parents('.panel');

		$panel.find('.panel-body').slideToggle();
		if($this.css('display') != 'none') {
			$panel.find('.panel-body input').focus();
		}
	});
	$('[data-toggle="tooltip"]').tooltip();
})






cartjs.add({
	id       : "1",
	name     : "Versace",
	price    : "830",
	quantity : "1"
})
cartjs.add({
	id       : "2",
	name     : "Versace",
	price    : "840",
	quantity : "1"
})
cartjs.add({
	id       : "3",
	name     : "Versace",
	price    : "850",
	quantity : "1"
})
cartjs.add({
	id       : "4",
	name     : "Versace",
	price    : "860",
	quantity : "1"
})






function toggleChevron(e) {
	$(e.target)
		.prev('.panel-heading')
		.find("i.indicator")
		.toggleClass('fa-caret-down fa-caret-right');
}
$('#accordion').on('hidden.bs.collapse', toggleChevron);
$('#accordion').on('shown.bs.collapse', toggleChevron);
