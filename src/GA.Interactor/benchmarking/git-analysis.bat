java -jar maat-1.0.jar -l "git-log.txt" -c git2 -a summary > summary.csv
java -jar maat-1.0.jar -l "git-log.txt" -c git2 > org-metrics.csv
java -jar maat-1.0.jar -l "git-log.txt" -c git2 -a coupling > coupling.csv
java -jar maat-1.0.jar -l "git-log.txt" -c git2 -a age > age.csv
java -jar maat-1.0.jar -l "git-log.txt" -c git2 -a abs-churn > abs-churn.csv
java -jar maat-1.0.jar -l "git-log.txt" -c git2 -a author-churn > author-churn.csv
java -jar maat-1.0.jar -l "git-log.txt" -c git2 -a entity-churn > entity-churn.csv
java -jar maat-1.0.jar -l "git-log.txt" -c git2 -a entity-ownership > entity-ownership.csv
java -jar maat-1.0.jar -l "git-log.txt" -c git2 -a entity-effort > entity-effort.csv
