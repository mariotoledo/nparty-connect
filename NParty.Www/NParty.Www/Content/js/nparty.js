function printRelatedPostsByLabels(currentArticleId, labels, listId) {
    console.log(labels);
    if (labels) {
        var doneCount = 0;
        var articles = [];
        for(var i = 0; i < labels.length; i++){
            $.get('/Nintendo/GetRelatedPosts/?label=' + labels[i], function (data) {
                if (data) {
                    $.each(data, function (j) {
                        articles.push(data[j]);
                    });

                    doneCount++;

                    if (doneCount == labels.length) {
                        articles = removeDuplicatedPostsFromRelated(currentArticleId, articles);
                        articles = shuffleArray(articles);
                        printRelatedArticles(articles, listId);
                    }
                }
            });
        }
    }
}

function removeDuplicatedPostsFromRelated(currentArticleId, relatedPosts) {
    var uniquePosts = [];
    $.each(relatedPosts, function (i, relatedEl) {
        var found = false;
        $.each(uniquePosts, function (i, uniqueEl) {
            if (uniqueEl.Id === relatedEl.Id && currentArticleId != relatedEl.id)
                found = true;
        });

        if(found == false)
            uniquePosts.push(relatedEl);
    });

    console.log(uniquePosts)

    return uniquePosts;
} 

function shuffleArray(array) {
    for (var j, x, i = array.length; i; j = Math.floor(Math.random() * i), x = array[--i], array[i] = array[j], array[j] = x);
    return array;
}

function printRelatedArticles(articles, listId) {
    $listEl = $('#' + listId);

    $listEl.html('');

    $.each(articles, function (i, article) {
        var appendable = '<li class="list-group-item">' +
                         '<a href="' + article.ArticleLink + '"></a>' +
                         '<div class="item-thumbnail-only">' +
                         '<a href="' + article.ArticleLink + '">' +
                         '<div class="item-thumbnail" style="margin-bottom: 0px; margin-right: 10px;">' +
                         '<div class="media-object-img-md" style="background-image: url(' + article.CoverImage + ')"></div>' +
                         '</div>' +
                         '<div style="padding-top: 20px; padding-bottom: 20px;">' + article.Title + '</div>' +
                         '<div style="clear: both;"></div></a></div></li>';
        $listEl.append(appendable);
    });
}