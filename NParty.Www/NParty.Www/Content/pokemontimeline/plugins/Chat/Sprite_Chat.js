function Chatrender() {
    this._id = "chat";
	this.input = $('<input>');
	this.init();
}

Chatrender.prototype = {
	init: function() {
        var self = this;
        this.input.attr("type", "text");
        this.input.addClass("chatbox");
       
        Menu.sceneReady("chatbox", function() {
            var div_textarea = Menu.getElement("chatbox", "textarea");
            div_textarea.append(self.input); 
            self.input.height(div_textarea.height());
            self.input.keypress(function(event) {
                if (event.which == 13) {
            	    var text = $(this).val();
    		    	self.sendText(text);
                }
    		});
        });
	},
	sendText: function(text) {
	    this.input.val("");
	    this.input.focus();
		this.rpg.emit("sendText", {text: text, event: this.rpg.player.id});
	},
   Core: {
	 displayText: function(data) {
     
		var event = this.rpg.getEventRenderById(data.event), el;

		if ($('#tooltip-' + data.event).length == 0) {
			this.rpg.addHtmlElement('<div id="tooltip" class="speech talk"></div>', -10, -50, event);
		}
		
		this.div = $(event.getElementById("tooltip"));
		this.div.text(data.text);
		this.div.fadeIn("fast").delay(4000).fadeOut("fast");
	 }
   },
   Event: {
   
   }
}