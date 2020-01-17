# Furesoft.Proxy
A Usefull Proxy for Parent Control.

## Planned Features:

- Block Sites based on Domains or Keyword
- Redirect Sites
- Password Secured Manager
- Simple to use Language to Block any kind of data like (block all images, ...)
- Log all visited websites
- Log Using Time

## Query Language Example

block domain google.* display "Google is prohibited" and
      keyword "porn" redirect "http://wikipedia.org" and
       keywords "hello", "world" display error

log usagetime to "user.txt"

