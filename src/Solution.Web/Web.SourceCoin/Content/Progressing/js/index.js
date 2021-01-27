(function () {  
    var percentage = $('#hd-percentage').val();
    var targetbonus = $('#hd-target').val();
    var receivedbonus = $('#hd-received').val();
    var currentTarget, countItemIndex;
    countItemIndex = 0;
    
    if(targetbonus <= 50000) {
        currentTarget = 5;
        //countItemIndex = 0;
    } else if (targetbonus > 50000 && targetbonus <= 300000){
        currentTarget = 4;
        //countItemIndex = 1;
    } else if (targetbonus > 300000 && targetbonus <= 600000) {
        currentTarget = 3;
        //countItemIndex = 2;
    } else if (targetbonus > 600000 && targetbonus <= 1000000) {
        currentTarget = 2;
        //countItemIndex = 3;
    }
    else if (targetbonus > 1000000 && targetbonus <= 1500000) {
        currentTarget = 1;
    } else {
       // currentTarget = 1;
    }
    // calulate percent
    var newtargetbonus = 0;
    var percentageOntarget = 0;
    var newpercentage;
    
    if (receivedbonus<=50000) {
        newtargetbonus = receivedbonus;
        percentageOntarget = newtargetbonus / targetbonus * 100;
        percentageOntarget = percentageOntarget / 100 * 20;
        newpercentage = 0 + percentageOntarget; 
    } else if (receivedbonus > 50000 && receivedbonus <= 300000) {
        newtargetbonus = receivedbonus;
        percentageOntarget = newtargetbonus / targetbonus * 100;
        percentageOntarget = percentageOntarget / 100 * 20;
        newpercentage = 20 + percentageOntarget; 
       
    } else if (receivedbonus > 300000 && receivedbonus <= 600000) {
        newtargetbonus = receivedbonus;
        percentageOntarget = newtargetbonus / targetbonus * 100;
        percentageOntarget = percentageOntarget / 100 * 20;
        newpercentage = 40 + percentageOntarget; 
    } else if (receivedbonus > 600000 && receivedbonus <= 1000000) {
        newtargetbonus = receivedbonus;
        percentageOntarget = newtargetbonus / targetbonus * 100;
        percentageOntarget = percentageOntarget / 100 * 20;
        newpercentage = 60 + percentageOntarget; 
    }
    else if (receivedbonus > 1000000 && receivedbonus <= 1500000) {
        newtargetbonus = receivedbonus;
        percentageOntarget = newtargetbonus / targetbonus * 100;
        percentageOntarget = percentageOntarget / 100 * 20;
        newpercentage = 80 + percentageOntarget; 
    } 
  var $point_arr, $points, $progress, $trigger, active, max, tracker, val;

  $trigger   = $('.trigger').first();
  $points    = $('.progress-points').first();
  $point_arr = $('.progress-point');
  $progress  = $('.progress').first();

    val = + $points.data('current') - currentTarget;
  
    max = $point_arr.length - 1;
   
  tracker = active = 0;

  function activate(index) {
    if (index !== active) {
      active       = index;
      var $_active = $point_arr.eq(active);
      
        $point_arr
            .removeClass('completed active')
            .slice(0, active).addClass('completed');
            //.addClass('completed');
            
      $_active.addClass('active');
        //return $progress.css('width', (index / (parseInt(max)) * percentage) + "%");
        //return $progress.css('width', ((parseInt(index) + parseInt(countItemIndex)) / (parseInt(max)) * percentage) + "%");
        
        return $progress.css('width', (max / (parseInt(max)) * newpercentage) + "%");
        //return $progress.css('width', newpercentage + "%");
    }
  };

  //$points.on('click', 'li', function(event) {
  //  var _index;
  //  _index  = $point_arr.index(this);
  //  tracker = _index === 0 ? 1 : _index === val ? 0 : tracker;
    
  //  return activate(_index);
  //});

  //$trigger.on('click', function() {
  //  return activate(tracker++ % 2 === 0 ? 0 : val);
  //});

  setTimeout((function() {
     
      return activate(val);
  }), 1000);

}).call(this);