function list_child_processes(){
    local ppid=$1;
    local current_children=$(pgrep -P $ppid);
    local local_child;
    if [ $? -eq 0 ];
    then
        for current_child in $current_children
        do
          local_child=$current_child;
          list_child_processes $local_child;
          echo $local_child;
        done;
    else
      return 0;
    fi;
}

ps 39285;
while [ $? -eq 0 ];
do
  sleep 1;
  ps 39285 > /dev/null;
done;

for child in $(list_child_processes 39303);
do
  echo killing $child;
  kill -s KILL $child;
done;
rm /Users/collinhaun/repo/captive-portal/src/bin/Debug/net6.0/84dc4fa98b0b47d4b2af1201e828e142.sh;
