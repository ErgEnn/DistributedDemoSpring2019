# Docker


Get the latest nginx image from docker hub
~~~
docker pull nginx
~~~


Build aurelia app
~~~
au build
~~~

Files are now in dist folder

Copy web content (aurelia) over to docker. Create dockerfile for this
~~~
FROM nginx
COPY dist /usr/share/nginx/html
~~~

Build the image (yes there is . at the end of the command)
~~~
docker build -t nginxaurelia .
~~~

Make a test run
~~~
docker run --name nginx  --rm -it -p 8080:80 nginxaurelia
~~~

Browse to http://localhost:8080 and test, that everything is working (don't forget your backend!).


Tag the image and push it to docker hub (use your own docker account!)
~~~
docker tag nginxaurelia akaver/nginxaurelia:test
docker push akaver/nginxaurelia:test
~~~

And then deploy it to hosting!
