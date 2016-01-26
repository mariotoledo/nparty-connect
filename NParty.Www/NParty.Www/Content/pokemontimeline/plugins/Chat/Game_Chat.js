function Chat() {
    
}

Chat.prototype = {

   Core: {
     loadMap: function() {
		var self = this;
		this.rpg.on("sendText", function(data) {
			
			self.render.send("currentMap", "displayText", {text: data.text, event: data.event});
		});
     }
   },
   Event: {
   
   }
}

if (typeof(exports) !== "undefined") {
	exports.plugin = Chat;
}