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

ps 45814;
while [ $? -eq 0 ];
do
  sleep 1;
  ps 45814 > /dev/null;
done;

for child in $(list_child_processes 45967);
do
  echo killing $child;
  kill -s KILL $child;
done;
rm /Users/collinhaun/repo/captive-portal/src/bin/Debug/net6.0/739a91aedee04c9e809b179c81b2cd12.sh;
